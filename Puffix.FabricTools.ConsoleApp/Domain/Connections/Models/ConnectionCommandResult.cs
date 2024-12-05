using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Connections.Models;

public class ConnectionCommandResult<ResultT>(string resultFilePath, long resultCount, ResultT result) :
    BaseCommandResult<ResultT>(resultFilePath, resultCount, result),
    IConnectionCommandResult<ResultT>
        where ResultT : class
{ }