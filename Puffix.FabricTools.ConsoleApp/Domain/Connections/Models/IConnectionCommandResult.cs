using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Connections.Models;

public interface IConnectionCommandResult<ResultT> : ICommandResult<ResultT>
    where ResultT : class
{
    public static IConnectionCommandResult<ResultT> CreateNew(string resultFilePath, long resultCount, ResultT result)
    {
        return new ConnectionCommandResult<ResultT>(resultFilePath, resultCount, result);
    }
}
