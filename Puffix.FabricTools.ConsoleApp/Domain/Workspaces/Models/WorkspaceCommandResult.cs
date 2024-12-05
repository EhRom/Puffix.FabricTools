using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class WorkspaceCommandResult<ResultT>(string resultFilePath, long resultCount, ResultT result) :
    BaseCommandResult<ResultT>(resultFilePath, resultCount, result),
    IWorkspaceCommandResult<ResultT>
        where ResultT : class
{ }
