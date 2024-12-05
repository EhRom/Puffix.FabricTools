using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Capacities.Models;

public class CapacityCommandResult<ResultT>(string resultFilePath, long resultCount, ResultT result) :
    BaseCommandResult<ResultT>(resultFilePath, resultCount, result),
    ICapacityCommandResult<ResultT>
        where ResultT : class
{ }