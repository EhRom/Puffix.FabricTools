using Puffix.Rest;

namespace Puffix.FabricTools.ConsoleApp.Infra.MsApiRest;

public class MsApiRestHttpRepository(IHttpClientFactory httpClientFactory) :
    RestHttpRepository<IMsApiRestHttpQueryInformation, IMsApiRestToken>(httpClientFactory),
    IMsApiRestHttpRepository
{
    public override IMsApiRestHttpQueryInformation BuildUnauthenticatedQuery(HttpMethod httpMethod, string apiUri, string queryPath, string queryParameters, string queryContent)
    {
        return MsApiRestHttpQueryInformation.CreateNewUnauthenticatedQuery(httpMethod, apiUri, queryPath, queryParameters, queryContent);
    }

    public override IMsApiRestHttpQueryInformation BuildAuthenticatedQuery(IMsApiRestToken token, HttpMethod httpMethod, string apiUri, string queryPath, string queryParameters, string queryContent)
    {
        return MsApiRestHttpQueryInformation.CreateNewAuthenticatedQuery(token, httpMethod, apiUri, queryPath, queryParameters, queryContent);
    }
}
