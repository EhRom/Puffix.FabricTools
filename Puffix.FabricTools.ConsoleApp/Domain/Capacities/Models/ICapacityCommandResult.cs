using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Capacities.Models;

public interface ICapacityCommandResult<ResultT> : ICommandResult<ResultT>
    where ResultT : class
{
    public static ICapacityCommandResult<ResultT> CreateNew(string resultFilePath, long resultCount, ResultT result)
    {
        return new CapacityCommandResult<ResultT>(resultFilePath, resultCount, result);
    }
}