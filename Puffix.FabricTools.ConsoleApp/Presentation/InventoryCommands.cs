using Puffix.ConsoleLogMagnifier;
using Puffix.FabricTools.ConsoleApp.Domain.Capacities;
using Puffix.FabricTools.ConsoleApp.Domain.Capacities.Models;
using Puffix.FabricTools.ConsoleApp.Domain.Connections;
using Puffix.FabricTools.ConsoleApp.Domain.Connections.Models;
using Puffix.FabricTools.ConsoleApp.Domain.Domains;
using Puffix.FabricTools.ConsoleApp.Domain.Domains.Models;
using Puffix.FabricTools.ConsoleApp.Domain.Gateways;
using Puffix.FabricTools.ConsoleApp.Domain.Gateways.Models;
using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using Puffix.FabricTools.ConsoleApp.Domain.Workspaces;
using Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;
using System;
using System.Diagnostics;

namespace Puffix.FabricTools.ConsoleApp.Presentation;

public class InventoryCommands
    (
        ICapacitiesService capacitiesService,
        IConnectionsService connectionsService,
        IDomainsService domainsService,
        IGatewaysService gatewaysService,
        IWorkspacesService workspacesService,
        IAdminWorkspacesService adminWorkspacesService
    )
{
    private readonly ICapacitiesService capacitiesService = capacitiesService;
    private readonly IConnectionsService connectionsService = connectionsService;
    private readonly IDomainsService domainsService = domainsService;
    private readonly IGatewaysService gatewaysService = gatewaysService;
    private readonly IWorkspacesService workspacesService = workspacesService;
    private readonly IAdminWorkspacesService adminWorkspacesService = adminWorkspacesService;

    public async Task SelectInventoryCommand()
    {
        ConsoleKey key;

        do
        {
            ConsoleHelper.WriteNewLine(2);
            ConsoleHelper.WriteInfo("Inventory menu.");
            ConsoleHelper.WriteNewLine(1);
            ConsoleHelper.Write("Press:");
            ConsoleHelper.Write("- C to list capacities.");
            ConsoleHelper.Write("- O to list connections.");
            ConsoleHelper.Write("- D to list domains.");
            ConsoleHelper.Write("- G to list gateways.");
            ConsoleHelper.Write("- I to list items in the tenant.");
            ConsoleHelper.Write("- W to natigate to workspaces menu.");
            ConsoleHelper.Write("- Escape to return to main menu.");

            ConsoleHelper.WriteNewLine(1);
            key = ConsoleHelper.ReadKey();

            if (key == ConsoleKey.Escape)
                ConsoleHelper.WriteInfo("Return to main menu");
            else if (key == ConsoleKey.C)
                await ListCapcities();
            else if (key == ConsoleKey.O)
                await ListConnections();
            else if (key == ConsoleKey.D)
                await ListDomains();
            else if (key == ConsoleKey.G)
                await ListGateways();
            else if (key == ConsoleKey.I)
                await ListTenantItems();
            else if (key == ConsoleKey.W)
                await SelectWorkspacesCommands();
            else
                ConsoleHelper.WriteWarning($"The key {key} is not a known command (for the moment :-) )");

        } while (key != ConsoleKey.Escape);
    }

    public async Task SelectWorkspacesCommands()
    {
        ConsoleKey key;

        do
        {
            ConsoleHelper.WriteNewLine(2);
            ConsoleHelper.Write("Press:");
            ConsoleHelper.Write("- L to list workspaces.");
            ConsoleHelper.Write("- D to list workspaces details.");
            ConsoleHelper.Write("- A to list workspaces (admin).");
            ConsoleHelper.Write("- W to get workspace details.");
            ConsoleHelper.Write("- X to get workspace (admin) with access details / role assignements.");
            ConsoleHelper.Write("- G to list Git connections.");
            ConsoleHelper.Write("- U to list role assignements / access details.");
            ConsoleHelper.Write("- I to list items in a workspace.");
            ConsoleHelper.Write("- J to get item access details / role assignements.");
            ConsoleHelper.Write("- Escape to return to main menu.");

            ConsoleHelper.WriteNewLine(1);
            key = ConsoleHelper.ReadKey();

            if (key == ConsoleKey.Escape)
                ConsoleHelper.WriteInfo("RReturn to main menu");
            else if (key == ConsoleKey.L)
                await ListWorkspaces();
            else if (key == ConsoleKey.D)
                await ListWorkspacesWithDetails();
            else if (key == ConsoleKey.A)
                await ListWorkspacesAsAdmin();
            else if (key == ConsoleKey.W)
                await GetWorkspace();
            else if (key == ConsoleKey.X)
                await GetWorkspaceAsAdminWithAccessDetails();
            else if (key == ConsoleKey.G)
                await ListGitConnectionsAsAdmin();
            else if (key == ConsoleKey.U)
                await ListWithRoleAssignements();
            else if (key == ConsoleKey.I)
                await GetWorkspaceItems();
            else if (key == ConsoleKey.J)
                await GetWorkspaceAdminItemWithAccessDetails();
            else
                ConsoleHelper.WriteWarning($"The key {key} is not a known command (for the moment :-) )");

        } while (key != ConsoleKey.Escape);
    }

    public async Task ListCapcities()
    {
        const string commandMessage = "List capacities";
        const string successMessage = "The capacities list";
        const string errorMessage = "listing the capacities";

        Func<Task<ICapacityCommandResult<CapacityList>>> command = capacitiesService.List;

        await BaseCommands.ExecuteCommand<ICapacityCommandResult<CapacityList>, CapacityList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListConnections()
    {
        const string commandMessage = "List connections";
        const string successMessage = "The connections list";
        const string errorMessage = "listing the connections";

        Func<Task<IConnectionCommandResult<ConnectionList>>> command = connectionsService.List;

        await BaseCommands.ExecuteCommand<IConnectionCommandResult<ConnectionList>, ConnectionList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListDomains()
    {
        const string commandMessage = "List domains";
        const string successMessage = "The domains list";
        const string errorMessage = "listing the domains";

        Func<Task<IDomainCommandResult<FabricDomainList>>> command = domainsService.List;

        await BaseCommands.ExecuteCommand<IDomainCommandResult<FabricDomainList>, FabricDomainList>(commandMessage, successMessage, errorMessage, command);
    }
    public async Task ListGateways()
    {
        const string commandMessage = "List gateways";
        const string successMessage = "The gateways list";
        const string errorMessage = "listing the gateways";

        Func<Task<IGatewayCommandResult<GatewayList>>> command = gatewaysService.List;

        await BaseCommands.ExecuteCommand<IGatewayCommandResult<GatewayList>, GatewayList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListTenantItems()
    {
        const string commandMessage = "List items";
        const string successMessage = "The items list";
        const string errorMessage = "listing the items";

        Func<Task<IWorkspaceCommandResult<FabricAdminItemList>>> command = adminWorkspacesService.ListItems;

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<FabricAdminItemList>, FabricAdminItemList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListWorkspaces()
    {
        const string commandMessage = "List workspaces";
        const string successMessage = "The workspaces list";
        const string errorMessage = "listing the workspaces";

        Func<Task<IWorkspaceCommandResult<WorkspaceList>>> command = workspacesService.List;

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<WorkspaceList>, WorkspaceList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListWorkspacesWithDetails()
    {
        const string commandMessage = "List workspaces with details";
        const string successMessage = "The workspaces with details list";
        const string errorMessage = "listing the workspaces with details";

        Func<Task<IWorkspaceCommandResult<WorkspaceList>>> command = workspacesService.ListWithDetails;

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<WorkspaceList>, WorkspaceList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListWithRoleAssignements()
    {
        const string commandMessage = "List workspaces with role role assignements";
        const string successMessage = "The workspaces with role assignements list";
        const string errorMessage = "listing the workspaces with details";

        Func<Task<IWorkspaceCommandResult<WorkspaceList>>> command = workspacesService.ListWithRoleAssignements;

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<WorkspaceList>, WorkspaceList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListWorkspacesAsAdmin()
    {
        const string commandMessage = "List workspaces (admin)";
        const string successMessage = "The workspaces (admin) list";
        const string errorMessage = "listing the workspaces (admin)";

        Func<Task<IWorkspaceCommandResult<AdminWorkspaceList>>> command = adminWorkspacesService.List;

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<AdminWorkspaceList>, AdminWorkspaceList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListGitConnectionsAsAdmin()
    {
        const string commandMessage = "List Git connections (admin)";
        const string successMessage = "The Git connections (admin) list";
        const string errorMessage = "listing the Git connections (admin)";

        Func<Task<IWorkspaceCommandResult<GitConnectionList>>> command = adminWorkspacesService.ListGitConnections;

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<GitConnectionList>, GitConnectionList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task GetWorkspace()
    {
        const string elementToGet = "workspace ID to get";

        Guid guid = BaseCommands.GetEnteredGuid(elementToGet);

        string commandMessage = $"Get the {guid} workspace details";
        string successMessage = $"The {guid} workspace details";
        string errorMessage = $"getting the {guid} workspace details";

        async Task<IWorkspaceCommandResult<Workspace>> command(string worksapceId) => await workspacesService.GetWorkspaceDetails(worksapceId);

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<Workspace>, Workspace>(commandMessage, successMessage, errorMessage, command, guid.ToString("D"));
    }

    public async Task GetWorkspaceAsAdminWithAccessDetails()
    {
        const string elementToGet = "workspace ID to get";

        Guid guid = BaseCommands.GetEnteredGuid(elementToGet);

        string commandMessage = $"Get the {guid} workspace (admin) with access details";
        string successMessage = $"The {guid} workspace (admin) with access details";
        string errorMessage = $"getting the {guid} workspace (admin) with access details";

        async Task<IWorkspaceCommandResult<AdminWorkspace>> command(string worksapceId) => await adminWorkspacesService.GetWorkspaceDetails(worksapceId);

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<AdminWorkspace>, AdminWorkspace>(commandMessage, successMessage, errorMessage, command, guid.ToString("D"));
    }

    public async Task GetWorkspaceItems()
    {
        const string elementToGet = "workspace ID from which to get elements";

        Guid guid = BaseCommands.GetEnteredGuid(elementToGet);

        string commandMessage = $"Get the {guid} workspace elements";
        string successMessage = $"The {guid} workspace elements";
        string errorMessage = $"getting the {guid} workspace elements";

        async Task<IWorkspaceCommandResult<FabricItemList>> command(string worksapceId) => await workspacesService.GetWorkspaceItems(worksapceId);

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<FabricItemList>, FabricItemList>(commandMessage, successMessage, errorMessage, command, guid.ToString("D"));
    }

    public async Task GetWorkspaceAdminItemWithAccessDetails()
    {
        const string workspaceText = "workspace ID of the item";

        Guid workspaceGuid = BaseCommands.GetEnteredGuid(workspaceText);

        const string itemText = "item ID from which to get role assignments";

        Guid itemGuid = BaseCommands.GetEnteredGuid(itemText);

        string commandMessage = $"Get the {itemGuid} item from {workspaceGuid} workspace role assignements";
        string successMessage = $"The {itemGuid} item from {workspaceGuid} workspace role assignements";
        string errorMessage = $"getting the {itemGuid} item from {workspaceGuid} workspace role assignements";

        async Task<IWorkspaceCommandResult<FabricAdminItem>> command(string worksapceId, string itemId) => await adminWorkspacesService.GetItemRoleAssignements(worksapceId, itemId);

        await BaseCommands.ExecuteCommand<IWorkspaceCommandResult<FabricAdminItem>, FabricAdminItem>(commandMessage, successMessage, errorMessage, command, workspaceGuid.ToString("D"), itemGuid.ToString("D"));
    }
}