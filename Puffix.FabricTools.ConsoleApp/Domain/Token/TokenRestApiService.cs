using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Puffix.FabricTools.ConsoleApp.Infra.Files;
using Puffix.FabricTools.ConsoleApp.Infra.MsApiRest;
using System.Text.Json.Nodes;

namespace Puffix.FabricTools.ConsoleApp.Domain.Token;

public class TokenRestApiService(IConfiguration configuration, IMemoryCache memoryCache, IFileService fileService) : ITokenRestApiService
{
    private readonly IConfiguration configuration = configuration;
    private readonly IMemoryCache memoryCache = memoryCache;
    private readonly IFileService fileService = fileService;

    private readonly Lazy<bool> authAsServicePrincipalLazy = new(() =>
    {
        return configuration.GetValue<bool>(nameof(authAsServicePrincipal));
    });

    private readonly Lazy<string> spnBaseAuthorityLazy = new(() =>
    {
        return configuration[nameof(spnBaseAuthority)]!;
    });

    private readonly Lazy<string[]> spnScopesLazy = new(() =>
    {
        return configuration.GetSection(nameof(spnScopes)).Get<string[]>() ?? [];
    });

    private readonly Lazy<string> userAuthorityLazy = new(() =>
    {
        return configuration[nameof(userAuthority)]!;
    });

    private readonly Lazy<string[]> userScopesLazy = new(() =>
    {
        return configuration.GetSection(nameof(userScopes)).Get<string[]>() ?? [];
    });

    private readonly Lazy<string> tenantIdLazy = new(() =>
    {
        return configuration[nameof(tenantId)]!;
    });

    private readonly Lazy<string> clientIdLazy = new(() =>
    {
        return configuration[nameof(clientId)]!;
    });

    private readonly Lazy<string> clientSecretLazy = new(() =>
    {
        return configuration[nameof(clientSecret)]!;
    });

    private readonly Lazy<string> redirectUriLazy = new(() =>
    {
        return configuration[nameof(redirectUri)]!;
    });

    private readonly Lazy<string> accessTokenLazy = new(() =>
    {
        return configuration[nameof(accessToken)]!;
    });

    private readonly Lazy<DateTimeOffset> accessTokenExpirationDateLazy = new(() =>
    {
        return configuration.GetValue<DateTimeOffset?>(nameof(accessTokenExpirationDate)) ?? DateTimeOffset.MinValue;
    });

    private bool authAsServicePrincipal => authAsServicePrincipalLazy.Value;
    private string spnBaseAuthority => spnBaseAuthorityLazy.Value;
    private string[] spnScopes => spnScopesLazy.Value;
    private string userAuthority => userAuthorityLazy.Value;
    private string[] userScopes => userScopesLazy.Value;
    private string tenantId => tenantIdLazy.Value;
    private string clientId => clientIdLazy.Value;
    private string clientSecret => clientSecretLazy.Value;
    private string redirectUri => redirectUriLazy.Value;
    private string accessToken => accessTokenLazy.Value;
    private DateTimeOffset accessTokenExpirationDate => accessTokenExpirationDateLazy.Value;

    public async Task<DateTimeOffset> GetToken()
    {
        AuthenticationResult token = authAsServicePrincipal ?
                    await GetTokenAsServicePrincipal() :
                    await GetTokenAsUser();

        SetTokenInCache(token.AccessToken, token.ExpiresOn);

        return token.ExpiresOn;
    }

    private async Task<AuthenticationResult> GetTokenAsServicePrincipal()
    {
        string spnAuthority = spnBaseAuthority.TrimEnd('/');
        spnAuthority = $"{spnAuthority}/{tenantId}";

        IConfidentialClientApplication application = ConfidentialClientApplicationBuilder.Create(clientId)
                     .WithClientSecret(clientSecret)
                     .WithAuthority(spnAuthority)
                     .WithTenantId(tenantId)
                     .Build();

        AuthenticationResult result = await application.AcquireTokenForClient(spnScopes)
                    .ExecuteAsync()
                    .ConfigureAwait(false);

        return result;
    }

    private async Task<AuthenticationResult> GetTokenAsUser()
    {
        IPublicClientApplication application = PublicClientApplicationBuilder.Create(clientId)
                     .WithAuthority(userAuthority)
                     .WithRedirectUri(redirectUri)
                     .Build();

        AuthenticationResult result = await application.AcquireTokenInteractive(userScopes)
                .ExecuteAsync()
                .ConfigureAwait(false);

        return result;
    }

    public async Task<bool> LoadToken()
    {
        bool validToken;
        if (!string.IsNullOrEmpty(accessToken) && accessTokenExpirationDate > DateTimeOffset.UtcNow)
        {
            SetTokenInCache(accessToken, accessTokenExpirationDate);
            validToken = true;
        }
        else
        {
            await EraseToken();
            validToken = false;
        }

        return validToken;
    }

    private void SetTokenInCache(string accessToken, DateTimeOffset accessTokenExpirationDate)
    {
        using ICacheEntry entry = memoryCache.CreateEntry(IMsApiRestToken.ACCESS_TOKEN_ENTRY_NAME);
        entry.Value = accessToken;
        entry.AbsoluteExpiration = accessTokenExpirationDate.ToUniversalTime();

        using ICacheEntry expirationEntry = memoryCache.CreateEntry(IMsApiRestToken.ACCESS_TOKEN_EXPIRATION_DATE_ENTRY_NAME);
        expirationEntry.Value = accessTokenExpirationDate.ToUniversalTime();
        expirationEntry.AbsoluteExpiration = accessTokenExpirationDate.ToUniversalTime();
    }

    public async Task SaveToken()
    {
        string accessTokenFromCache = memoryCache.Get<string>(IMsApiRestToken.ACCESS_TOKEN_ENTRY_NAME) ?? string.Empty;
        DateTimeOffset accessTokenExpirationDateFromCache = memoryCache.Get<DateTimeOffset?>(IMsApiRestToken.ACCESS_TOKEN_EXPIRATION_DATE_ENTRY_NAME) ?? DateTimeOffset.MinValue;

        if (!string.IsNullOrEmpty(accessTokenFromCache))
            await ModifyTokenConfiguration(accessTokenFromCache, accessTokenExpirationDateFromCache);
    }

    public async Task EraseToken()
    {
        await ModifyTokenConfiguration(string.Empty, DateTimeOffset.MinValue);
    }

    public async Task SwitchAuthenticationMode()
    {
        throw new NotImplementedException();
    }

    private async Task ModifyTokenConfiguration(string accessToken, DateTimeOffset accessTokenExpirationDate)
    {
        if (configuration is IConfigurationRoot)
        {
            (JsonNode? jsonConfigurationContent, string configurationFilePath) = await LoadConfiguration((configuration as IConfigurationRoot)!);

            if (jsonConfigurationContent is not null)
            {
                jsonConfigurationContent[IMsApiRestToken.ACCESS_TOKEN_ENTRY_NAME] = accessToken;
                jsonConfigurationContent[IMsApiRestToken.ACCESS_TOKEN_EXPIRATION_DATE_ENTRY_NAME] = accessTokenExpirationDate;

                await PersistConfiguration(configurationFilePath, jsonConfigurationContent!);
            }
        }
    }

    private async Task<(JsonNode?, string)> LoadConfiguration(IConfigurationRoot configurationRoot)
    {
        // Select configuration file
        FileConfigurationProvider provider = configurationRoot.Providers
               .Where(p => p is FileConfigurationProvider)
               .Select(p => (FileConfigurationProvider)p)
               .Last();

        JsonNode? jsonConfigurationContent = default;
        string configurationFilePath = provider.Source.Path ?? string.Empty;

        if (provider is not null && !string.IsNullOrEmpty(configurationFilePath))
        {
            string configurationContent = await fileService.LoadContent(configurationFilePath);
            jsonConfigurationContent = JsonNode.Parse(configurationContent);
        }

        return (jsonConfigurationContent, configurationFilePath);
    }

    private async Task PersistConfiguration(string configurationFilePath, JsonNode jsonConfigurationContent)
    {
        string configurationContent = jsonConfigurationContent.ToJsonString();
        await fileService.SaveContent(configurationFilePath, configurationContent);
    }
}