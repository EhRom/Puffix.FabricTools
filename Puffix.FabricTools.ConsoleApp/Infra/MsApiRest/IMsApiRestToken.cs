using Puffix.Rest;

namespace Puffix.FabricTools.ConsoleApp.Infra.MsApiRest;

public interface IMsApiRestToken : IHeaderToken
{
    public const string ACCESS_TOKEN_ENTRY_NAME = "accessToken";

    public const string ACCESS_TOKEN_EXPIRATION_DATE_ENTRY_NAME = "accessTokenExpirationDate";
}