using Puffix.ConsoleLogMagnifier;
using Puffix.FabricTools.ConsoleApp.Domain.Workspaces;
using Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

namespace Puffix.FabricTools.ConsoleApp.Presentation;

public class ActionsCommands(IWorkspacesService workspacesService)
{
    private readonly IWorkspacesService workspacesService = workspacesService;

    public async Task SelectActionCommand()
    {
        ConsoleKey key;

        do
        {
            ConsoleHelper.WriteNewLine(2);
            ConsoleHelper.WriteInfo("Actions menu.");
            ConsoleHelper.Write(ConsoleColor.DarkYellow, "!under construction");
            ConsoleHelper.WriteNewLine(1);
            ConsoleHelper.Write("Press:");
            //ConsoleHelper.Write("- R to assign roles from a file.");
            ConsoleHelper.Write("- A to assign a workspace to a capacity.");
            ConsoleHelper.Write("- B to assign a workspace list to a capacity.");
            ConsoleHelper.Write("- U to unassign a workspace from a capacity.");
            ConsoleHelper.Write("- V to unassign a workspace list from a capacity.");
            ConsoleHelper.Write("- Escape to return to main menu.");

            ConsoleHelper.WriteNewLine(1);
            key = ConsoleHelper.ReadKey();

            if (key == ConsoleKey.Escape)
                ConsoleHelper.WriteInfo("Return to main menu");
            //else if (key == ConsoleKey.R)
            //    await AddRoleAssignments();
            else if (key == ConsoleKey.A)
                await AssignWorkspaceToCapacity();
            else if (key == ConsoleKey.B)
                await AssignWorkspaceCollectionToCapacity();
            else if (key == ConsoleKey.U)
                await UnassignWorkspaceToCapacity();
            else if (key == ConsoleKey.V)
                await UnassignWorkspaceCollectionToCapacity();
            else
                ConsoleHelper.WriteWarning($"The key {key} is not a known command (for the moment :-) )");

        } while (key != ConsoleKey.Escape);
    }

    public async Task AddRoleAssignments()
    {
        const string elementToGet = "role assignements configuration";

        string path = BaseCommands.GetEnteredPath(elementToGet);

        await Task.Delay(100);
    }

    // TODO set user as admin (json format...)
    // https://learn.microsoft.com/en-us/rest/api/fabric/core/workspaces/update-workspace-role-assignment?tabs=HTTP
    // https://learn.microsoft.com/en-us/rest/api/fabric/core/workspaces/provision-identity?tabs=HTTP
    // https://learn.microsoft.com/en-us/rest/api/fabric/core/workspaces/list-workspace-role-assignments?tabs=HTTP

    public async Task AssignWorkspaceToCapacity()
    {
        const string itemText = "capacity ID to be assigned";

        Guid capacityGuid = BaseCommands.GetEnteredGuid(itemText);

        const string workspaceText = "workspace ID to assign";

        Guid workspaceGuid = BaseCommands.GetEnteredGuid(workspaceText);

        string commandMessage = $"Assign the {workspaceGuid} workspace to the {capacityGuid} capacity";
        string successMessage = $"The {workspaceGuid} workspace is assgined to the {capacityGuid} capacity. The updated workspace";
        string errorMessage = $"assigning the {workspaceGuid} workspace to the {capacityGuid} capacity";

        async Task<IWorkspaceCommandResult<Workspace>> command(string capacityId, string workspaceId) => await workspacesService.AssignWorkspaceToCapacity(capacityId, workspaceId);

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<Workspace>, Workspace>(commandMessage, successMessage, errorMessage, command, capacityGuid.ToString("D"), workspaceGuid.ToString("D"));
    }

    public async Task AssignWorkspaceCollectionToCapacity()
    {
        const string elementToGet = "capacity and workspace collection";
        string configurationFilePath = BaseCommands.GetEnteredPath(elementToGet);

        if (string.IsNullOrEmpty(configurationFilePath))
        {
            ConsoleHelper.WriteError("A valid file path is required to assign a workspace list to a capacity.");
            return;
        }

        string commandMessage = $"Assign a list of workspace to a capacity";
        string successMessage = $"The workspaces are assgined to the capacity. The updated workspace collection";
        string errorMessage = $"assigning a list of workspace to a capacity";

        async Task<IWorkspaceCommandResult<WorkspaceList>> command(string configurationFilePath) => await workspacesService.AssignWorkspaceCollectionToCapacity(configurationFilePath);

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<WorkspaceList>, WorkspaceList>(commandMessage, successMessage, errorMessage, command, configurationFilePath);
    }

    public async Task UnassignWorkspaceToCapacity()
    {
        const string workspaceText = "workspace ID to unassign";

        Guid workspaceGuid = BaseCommands.GetEnteredGuid(workspaceText);

        string commandMessage = $"Unssign the {workspaceGuid} workspace from its capacity";
        string successMessage = $"The {workspaceGuid} workspace is unassgined from its capacity. The updated workspace";
        string errorMessage = $"assigning the {workspaceGuid} workspace from its capacity";

        async Task<IWorkspaceCommandResult<Workspace>> command(string workspaceId) => await workspacesService.UnassignWorkspaceToCapacity(workspaceId);

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<Workspace>, Workspace>(commandMessage, successMessage, errorMessage, command, workspaceGuid.ToString("D"));
    }

    public async Task UnassignWorkspaceCollectionToCapacity()
    {
        const string elementToGet = "capacity and workspace collection";
        string configurationFilePath = BaseCommands.GetEnteredPath(elementToGet);

        if (string.IsNullOrEmpty(configurationFilePath))
        {
            ConsoleHelper.WriteError("A valid file path is required to unassign a workspace list from a capacity.");
            return;
        }

        string commandMessage = $"Unssign a list of workspace from a capacity";
        string successMessage = $"The workspaces are unassgined from the capacity. The updated workspace collection";
        string errorMessage = $"unassigning a list of workspace from a capacity";

        async Task<IWorkspaceCommandResult<WorkspaceList>> command(string configurationFilePath) => await workspacesService.UnassignWorkspaceCollectionToCapacity(configurationFilePath);

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<WorkspaceList>, WorkspaceList>(commandMessage, successMessage, errorMessage, command, configurationFilePath);
    }
}
