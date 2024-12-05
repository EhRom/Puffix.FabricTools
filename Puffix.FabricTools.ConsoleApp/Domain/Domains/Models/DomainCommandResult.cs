using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Domains.Models;

public class DomainCommandResult<ResultT>(string resultFilePath, long resultCount, ResultT result) :
    BaseCommandResult<ResultT>(resultFilePath, resultCount, result),
    IDomainCommandResult<ResultT>
        where ResultT : class
{ }
