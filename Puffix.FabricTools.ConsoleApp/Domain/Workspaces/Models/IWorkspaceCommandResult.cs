using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public interface IWorkspaceCommandResult<ResultT> : ICommandResult<ResultT>
    where ResultT : class
{
    public static IWorkspaceCommandResult<ResultT> CreateNew(string resultFilePath, long resultCount, ResultT result)
    {
        return new WorkspaceCommandResult<ResultT>(resultFilePath, resultCount, result);
    }
}
