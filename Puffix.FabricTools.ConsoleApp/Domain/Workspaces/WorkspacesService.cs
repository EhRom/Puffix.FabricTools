using Microsoft.Extensions.Configuration;
using Puffix.FabricTools.ConsoleApp.Domain.RestApi;
using Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;
using Puffix.FabricTools.ConsoleApp.Infra.Files;
using Puffix.FabricTools.ConsoleApp.Infra.MsApiRest;
using System.Text.Json;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces;

public class WorkspacesService(IConfiguration configuration, IMsApiRestToken token, IMsApiRestHttpRepository httpRepository, IFileService fileService) :
    BaseRestFabricService(configuration, token, httpRepository, fileService),
    IWorkspacesService
{
    private const string BASE_COMMAND = "workspaces";
    private const string BASE_FILE_NAME = "workspaces";

    private const string ROLES_COMMAND_PART = "roleAssignments";
    private const string ITEMS_COMMAND_PART = "items";

    private const string ASSIGN_TO_CAPACITY_COMMAND_PART = "assignToCapacity";
    private const string UNASSIGN_TO_CAPACITY_COMMAND_PART = "unassignFromCapacity";

    public async Task<IWorkspaceCommandResult<WorkspaceList>> List()
    {
        WorkspaceList workspaceList = await CoreList();

        string filePath = await SaveContent(BASE_FILE_NAME, workspaceList);

        return IWorkspaceCommandResult<WorkspaceList>.CreateNew(filePath, workspaceList.Elements.Count, workspaceList);
    }

    public async Task<IWorkspaceCommandResult<Workspace>> GetWorkspaceDetails(string workspaceId)
    {
        Workspace workspace = await CoreGetWorkspaceDetails(workspaceId);

        RoleAssignementList roleAssignementList = await CoreGetWorkspaceRoleAssignements(workspace.Id);
        workspace.RoleAssignements = roleAssignementList;

        string fileName = BASE_FILE_NAME.TrimEnd('s');
        fileName = $"{fileName}-{workspaceId}-";

        string filePath = await SaveContent(fileName, workspace);

        return IWorkspaceCommandResult<Workspace>.CreateNew(filePath, 1, workspace);
    }

    public async Task<IWorkspaceCommandResult<WorkspaceList>> ListWithDetails()
    {
        WorkspaceList workspaceList = await CoreList();

        WorkspaceList workspaceListWithDetails = new WorkspaceList();

        foreach (Workspace workspace in workspaceList.Elements)
        {
            Workspace workspaceWithDetails = await CoreGetWorkspaceDetails(workspace.Id);
            workspaceListWithDetails.Elements.Add(workspaceWithDetails);
        }

        string filePath = await SaveContent(BASE_FILE_NAME, workspaceListWithDetails);

        return IWorkspaceCommandResult<WorkspaceList>.CreateNew(filePath, workspaceList.Elements.Count, workspaceListWithDetails);
    }

    public async Task<IWorkspaceCommandResult<WorkspaceList>> ListWithRoleAssignements()
    {
        WorkspaceList workspaceList = await CoreList();

        WorkspaceList workspaceListWithDetails = new WorkspaceList();

        foreach (Workspace workspace in workspaceList.Elements)
        {
            RoleAssignementList roleAssignementList = await CoreGetWorkspaceRoleAssignements(workspace.Id);

            Workspace workspaceWithDetails = await CoreGetWorkspaceDetails(workspace.Id);

            workspaceWithDetails.RoleAssignements = roleAssignementList;
            workspaceListWithDetails.Elements.Add(workspaceWithDetails);
        }

        string filePath = await SaveContent(BASE_FILE_NAME, workspaceListWithDetails);

        return IWorkspaceCommandResult<WorkspaceList>.CreateNew(filePath, workspaceList.Elements.Count, workspaceListWithDetails);
    }

    public async Task<IWorkspaceCommandResult<FabricItemList>> GetWorkspaceItems(string workspaceId)
    {
        FabricItemList itemList = await CoreGetWorkspaceItems(workspaceId);

        string fileName = BASE_FILE_NAME.TrimEnd('s');
        fileName = $"{fileName}-{workspaceId}-{ITEMS_COMMAND_PART}-";

        string filePath = await SaveContent(fileName, itemList);

        return IWorkspaceCommandResult<FabricItemList>.CreateNew(filePath, 1, itemList);
    }

    public async Task<IWorkspaceCommandResult<Workspace>> AssignWorkspaceToCapacity(string capacityId, string workspaceId)
    {
        await CoreAssignWorkspaceToCapacity(capacityId, workspaceId);

        Workspace updatedWorkspace = await CoreGetWorkspaceDetails(workspaceId);
       
        string fileName = BASE_FILE_NAME.TrimEnd('s');
        fileName = $"{fileName}-{workspaceId}-";

        string filePath = await SaveContent(fileName, updatedWorkspace);

        return IWorkspaceCommandResult<Workspace>.CreateNew(filePath, 1, updatedWorkspace);
    }

    public async Task<IWorkspaceCommandResult<WorkspaceList>> AssignWorkspaceCollectionToCapacity(string queryFilePath)
    {
        WorkspaceList resultWorkspaceList = new WorkspaceList();
        AssignWorkpsaceToCapacityQuery query = await fileService.LoadJsonContent<AssignWorkpsaceToCapacityQuery>(queryFilePath);

        foreach (string workspaceId in query.WorkpsaceIdCollection)
        {
            await CoreAssignWorkspaceToCapacity(query.CapacityId, workspaceId);

            Workspace updatedWorkspace = await CoreGetWorkspaceDetails(workspaceId);
            resultWorkspaceList.Elements.Add(updatedWorkspace);
        }

        string fileName = $"updated-{BASE_FILE_NAME}";
        string filePath = await SaveContent(fileName, resultWorkspaceList);

        return IWorkspaceCommandResult<WorkspaceList>.CreateNew(filePath, resultWorkspaceList.Elements.Count, resultWorkspaceList);
    }

    public async Task<IWorkspaceCommandResult<Workspace>> UnassignWorkspaceToCapacity(string workspaceId)
    {
        await CoreUnassignWorkspaceToCapacity(workspaceId);

        Workspace updatedWorkspace = await CoreGetWorkspaceDetails(workspaceId);

        string fileName = BASE_FILE_NAME.TrimEnd('s');
        fileName = $"{fileName}-{workspaceId}-";

        string filePath = await SaveContent(fileName, updatedWorkspace);

        return IWorkspaceCommandResult<Workspace>.CreateNew(filePath, 1, updatedWorkspace);
    }

    public async Task<IWorkspaceCommandResult<WorkspaceList>> UnassignWorkspaceCollectionToCapacity(string queryFilePath)
    {
        WorkspaceList resultWorkspaceList = new WorkspaceList();
        UnassignWorkpsaceToCapacityQuery query = await fileService.LoadJsonContent<UnassignWorkpsaceToCapacityQuery>(queryFilePath);

        foreach (string workspaceId in query.WorkpsaceIdCollection)
        {
            await CoreUnassignWorkspaceToCapacity(workspaceId);

            Workspace updatedWorkspace = await CoreGetWorkspaceDetails(workspaceId);
            resultWorkspaceList.Elements.Add(updatedWorkspace);
        }

        string fileName = $"updated-{BASE_FILE_NAME}";
        string filePath = await SaveContent(fileName, resultWorkspaceList);

        return IWorkspaceCommandResult<WorkspaceList>.CreateNew(filePath, resultWorkspaceList.Elements.Count, resultWorkspaceList);
    }

    private async Task<WorkspaceList> CoreList()
    {
        return await GetElementCollection<WorkspaceList, Workspace>(BASE_COMMAND);
    }

    private async Task<Workspace> CoreGetWorkspaceDetails(string workspaceId)
    {
        string command = $"{BASE_COMMAND}/{workspaceId}";

        return await GetElement<Workspace>(command);
    }

    private async Task<RoleAssignementList> CoreGetWorkspaceRoleAssignements(string workspaceId)
    {
        string command = $"{BASE_COMMAND}/{workspaceId}/{ROLES_COMMAND_PART}";

        return await GetElementCollection<RoleAssignementList, RoleAssignement>(command);
    }

    private async Task<FabricItemList> CoreGetWorkspaceItems(string workspaceId)
    {
        string command = $"{BASE_COMMAND}/{workspaceId}/{ITEMS_COMMAND_PART}";

        return await GetElementCollection<FabricItemList, FabricItem>(command);
    }

    private async Task CoreAssignWorkspaceToCapacity(string capacityId, string workspaceId)
    {
        string command = $"{BASE_COMMAND}/{workspaceId}/{ASSIGN_TO_CAPACITY_COMMAND_PART}";

        string queryContent = JsonSerializer.Serialize(CapacityQueryContent.CreateNew(capacityId));

        await PostElement(command, queryContent);
    }

    private async Task CoreUnassignWorkspaceToCapacity(string workspaceId)
    {
        string command = $"{BASE_COMMAND}/{workspaceId}/{UNASSIGN_TO_CAPACITY_COMMAND_PART}";

        await PostElement(command, string.Empty);
    }
}