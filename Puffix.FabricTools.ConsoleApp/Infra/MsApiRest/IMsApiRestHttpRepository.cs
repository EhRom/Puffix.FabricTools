using Puffix.Rest;

namespace Puffix.FabricTools.ConsoleApp.Infra.MsApiRest;

public interface IMsApiRestHttpRepository : IRestHttpRepository<IMsApiRestHttpQueryInformation, IMsApiRestToken> { }