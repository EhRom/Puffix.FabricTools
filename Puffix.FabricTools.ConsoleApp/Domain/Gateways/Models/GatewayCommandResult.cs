using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Gateways.Models;

public class GatewayCommandResult<ResultT>(string resultFilePath, long resultCount, ResultT result) :
    BaseCommandResult<ResultT>(resultFilePath, resultCount, result),
    IGatewayCommandResult<ResultT>
        where ResultT : class
{ }