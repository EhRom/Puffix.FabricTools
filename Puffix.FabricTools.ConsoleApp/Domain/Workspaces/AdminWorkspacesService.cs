using Microsoft.Extensions.Configuration;
using Puffix.FabricTools.ConsoleApp.Domain.RestApi;
using Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;
using Puffix.FabricTools.ConsoleApp.Infra.Files;
using Puffix.FabricTools.ConsoleApp.Infra.MsApiRest;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces;

public class AdminWorkspacesService(IConfiguration configuration, IMsApiRestToken token, IMsApiRestHttpRepository httpRepository, IFileService fileService) :
    BaseRestFabricService(configuration, token, httpRepository, fileService),
    IAdminWorkspacesService
{
    private const string BASE_COMMAND = "admin/workspaces";
    private const string BASE_FILE_NAME = "admin_workspaces";

    private const string GIT_CONNECTION_COMMAND = $"{BASE_COMMAND}/discoverGitConnections";
    private const string GIT_CONNECTION_BASE_FILE_NAME = "git_connections";

    public async Task<IWorkspaceCommandResult<AdminWorkspaceList>> List()
    {
        AdminWorkspaceList workspaceList = await GetElementCollection<AdminWorkspaceList, AdminWorkspace>(BASE_COMMAND);

        string filePath = await SaveContent(BASE_FILE_NAME, workspaceList);
        return IWorkspaceCommandResult<AdminWorkspaceList>.CreateNew(filePath, workspaceList.Elements.Count, workspaceList);
    }

    public async Task<IWorkspaceCommandResult<AdminWorkspace>> GetWorkspaceDetails(string workspaceId)
    {
        AdminWorkspace workspace = await CoreGetWorkspaceDetails(workspaceId);

        if (workspace.State != "Deleted")
        {
            WorkspaceAccessList workspaceAccessList = await CoreGetWorkspaceAccesses(workspace.Id);
            workspace.AccessDetails = workspaceAccessList.Elements;
        }

        string fileName = BASE_FILE_NAME.TrimEnd('s');
        fileName = $"{fileName}-{workspaceId}-";

        string filePath = await SaveContent(fileName, workspace);

        return IWorkspaceCommandResult<AdminWorkspace>.CreateNew(filePath, 1, workspace);
    }

    public async Task<IWorkspaceCommandResult<GitConnectionList>> ListGitConnections()
    {
        GitConnectionList gitConnectionList = await GetElementCollection<GitConnectionList, GitConnection>(GIT_CONNECTION_COMMAND);

        string filePath = await SaveContent(GIT_CONNECTION_BASE_FILE_NAME, gitConnectionList);
        return IWorkspaceCommandResult<GitConnectionList>.CreateNew(filePath, gitConnectionList.Elements.Count, gitConnectionList);
    }

    private async Task<AdminWorkspace> CoreGetWorkspaceDetails(string workspaceId)
    {
        string command = $"{BASE_COMMAND}/{workspaceId}";
        return await GetElement<AdminWorkspace>(command);
    }

    private async Task<WorkspaceAccessList> CoreGetWorkspaceAccesses(string workspaceId)
    {
        string command = $"{BASE_COMMAND}/{workspaceId}/users";
        return await GetElement<WorkspaceAccessList>(command);
    }
}
