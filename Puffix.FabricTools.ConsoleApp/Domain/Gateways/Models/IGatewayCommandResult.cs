using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Gateways.Models;

public interface IGatewayCommandResult<ResultT> : ICommandResult<ResultT>
    where ResultT : class
{
    public static IGatewayCommandResult<ResultT> CreateNew(string resultFilePath, long resultCount, ResultT result)
    {
        return new GatewayCommandResult<ResultT>(resultFilePath, resultCount, result);
    }
}