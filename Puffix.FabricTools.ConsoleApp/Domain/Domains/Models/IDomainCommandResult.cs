using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Domains.Models;

public interface IDomainCommandResult<ResultT> : ICommandResult<ResultT>
    where ResultT : class
{
    public static IDomainCommandResult<ResultT> CreateNew(string resultFilePath, long resultCount, ResultT result)
    {
        return new DomainCommandResult<ResultT>(resultFilePath, resultCount, result);
    }
}
