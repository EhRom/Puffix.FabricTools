using Puffix.Rest;

namespace Puffix.FabricTools.ConsoleApp.Infra.MsApiRest;

public class MsApiRestHttpQueryInformation(HttpMethod httpMethod, IMsApiRestToken? token, IDictionary<string, string> headers, string baseUri, string queryPath, string queryParameters, string queryContent) :
    QueryInformation<IMsApiRestToken>(httpMethod, token, headers, baseUri, queryPath, queryParameters, queryContent),
    IMsApiRestHttpQueryInformation
{
    public static IMsApiRestHttpQueryInformation CreateNewUnauthenticatedQuery(HttpMethod httpMethod, string apiUri, string queryPath, string queryParameters, string queryContent)
    {
        return new MsApiRestHttpQueryInformation(httpMethod, default, new Dictionary<string, string>(), apiUri, queryPath, queryParameters, queryContent);
    }

    public static IMsApiRestHttpQueryInformation CreateNewAuthenticatedQuery(IMsApiRestToken token, HttpMethod httpMethod, string apiUri, string queryPath, string queryParameters, string queryContent)
    {
        return new MsApiRestHttpQueryInformation(httpMethod, token, new Dictionary<string, string>(), apiUri, queryPath, queryParameters, queryContent);
    }
}