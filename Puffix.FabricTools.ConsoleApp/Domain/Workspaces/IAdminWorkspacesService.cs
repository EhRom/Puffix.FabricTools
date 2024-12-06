using Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces;

public interface IAdminWorkspacesService
{
    Task<IWorkspaceCommandResult<AdminWorkspaceList>> List();

    Task<IWorkspaceCommandResult<AdminWorkspace>> GetWorkspaceDetails(string workspaceId);

    Task<IWorkspaceCommandResult<GitConnectionList>> ListGitConnections();

    Task<IWorkspaceCommandResult<FabricAdminItemList>> ListItems();
}