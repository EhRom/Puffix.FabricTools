using Microsoft.Extensions.Caching.Memory;

namespace Puffix.FabricTools.ConsoleApp.Infra.MsApiRest;

public class MsApiRestToken(IMemoryCache memoryCache) : IMsApiRestToken
{
    private readonly IMemoryCache memoryCache = memoryCache;

    public IDictionary<string, string> GetHeaders()
    {
        if (!memoryCache.TryGetValue(IMsApiRestToken.ACCESS_TOKEN_ENTRY_NAME, out string? accessToken) ||
            string.IsNullOrEmpty(accessToken))
            throw new Exception("The access token is not set. Set the token in cache prior to get headers");

        return new Dictionary<string, string>
        {
            { "Authorization",  $"Bearer {accessToken}" }
        };
    }
}