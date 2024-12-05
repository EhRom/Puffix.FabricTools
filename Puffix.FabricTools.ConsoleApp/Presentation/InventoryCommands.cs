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
            ConsoleHelper.Write("- W to natigate to workspaces menu.");
            ConsoleHelper.Write("- Escape to return to main menu.");

            ConsoleHelper.WriteNewLine(1);

            key = Console.ReadKey().Key;
            ConsoleHelper.ClearLastLines();
            //ConsoleHelper.ClearLastCharacters(1);

            if (key == ConsoleKey.Escape)
                ConsoleHelper.WriteInfo("RReturn to main menu");
            else if (key == ConsoleKey.C)
                await ListCapcities();
            else if (key == ConsoleKey.O)
                await ListConnections();
            else if (key == ConsoleKey.D)
                await ListDomains();
            else if (key == ConsoleKey.G)
                await ListGateways();
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
            ConsoleHelper.Write("- X to get workspace (admin) with access details.");
            ConsoleHelper.Write("- G to list Git connections.");
            ConsoleHelper.Write("- U to list role assignements.");
            ConsoleHelper.Write("- I to list items in a workspace.");
            ConsoleHelper.Write("- Escape to return to main menu.");

            ConsoleHelper.WriteNewLine(1);

            key = Console.ReadKey().Key;
            ConsoleHelper.ClearLastLines();
            //ConsoleHelper.ClearLastCharacters(1);

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

        await ExecuteCommand<ICapacityCommandResult<CapacityList>, CapacityList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListConnections()
    {
        const string commandMessage = "List connections";
        const string successMessage = "The connections list";
        const string errorMessage = "listing the connections";

        Func<Task<IConnectionCommandResult<ConnectionList>>> command = connectionsService.List;

        await ExecuteCommand<IConnectionCommandResult<ConnectionList>, ConnectionList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListDomains()
    {
        const string commandMessage = "List domains";
        const string successMessage = "The domains list";
        const string errorMessage = "listing the domains";

        Func<Task<IDomainCommandResult<FabricDomainList>>> command = domainsService.List;

        await ExecuteCommand<IDomainCommandResult<FabricDomainList>, FabricDomainList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListGateways()
    {
        const string commandMessage = "List gateways";
        const string successMessage = "The gateways list";
        const string errorMessage = "listing the gateways";

        Func<Task<IGatewayCommandResult<GatewayList>>> command = gatewaysService.List;

        await ExecuteCommand<IGatewayCommandResult<GatewayList>, GatewayList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListWorkspaces()
    {
        const string commandMessage = "List workspaces";
        const string successMessage = "The workspaces list";
        const string errorMessage = "listing the workspaces";

        Func<Task<IWorkspaceCommandResult<WorkspaceList>>> command = workspacesService.List;

        await ExecuteCommand<IWorkspaceCommandResult<WorkspaceList>, WorkspaceList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListWorkspacesWithDetails()
    {
        const string commandMessage = "List workspaces with details";
        const string successMessage = "The workspaces with details list";
        const string errorMessage = "listing the workspaces with details";

        Func<Task<IWorkspaceCommandResult<WorkspaceList>>> command = workspacesService.ListWithDetails;

        await ExecuteCommand<IWorkspaceCommandResult<WorkspaceList>, WorkspaceList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListWithRoleAssignements()
    {
        const string commandMessage = "List workspaces with role role assignements";
        const string successMessage = "The workspaces with role assignements list";
        const string errorMessage = "listing the workspaces with details";

        Func<Task<IWorkspaceCommandResult<WorkspaceList>>> command = workspacesService.ListWithRoleAssignements;

        await ExecuteCommand<IWorkspaceCommandResult<WorkspaceList>, WorkspaceList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListWorkspacesAsAdmin()
    {
        const string commandMessage = "List workspaces (admin)";
        const string successMessage = "The workspaces (admin) list";
        const string errorMessage = "listing the workspaces (admin)";

        Func<Task<IWorkspaceCommandResult<AdminWorkspaceList>>> command = adminWorkspacesService.List;

        await ExecuteCommand<IWorkspaceCommandResult<AdminWorkspaceList>, AdminWorkspaceList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task ListGitConnectionsAsAdmin()
    {
        const string commandMessage = "List Git connections (admin)";
        const string successMessage = "The Git connections (admin) list";
        const string errorMessage = "listing the Git connections (admin)";

        Func<Task<IWorkspaceCommandResult<GitConnectionList>>> command = adminWorkspacesService.ListGitConnections;

        await ExecuteCommand<IWorkspaceCommandResult<GitConnectionList>, GitConnectionList>(commandMessage, successMessage, errorMessage, command);
    }

    public async Task GetWorkspace()
    {
        const string elementToGet = "workspace ID to get";

        Guid guid = GetEnteredGuid(elementToGet);

        string commandMessage = $"Get the {guid} workspace details";
        string successMessage = $"The {guid} workspace details";
        string errorMessage = $"getting the {guid} workspace details";

        async Task<IWorkspaceCommandResult<Workspace>> command(string worksapceId) => await workspacesService.GetWorkspaceDetails(worksapceId);

        await ExecuteCommand<IWorkspaceCommandResult<Workspace>, Workspace>(commandMessage, successMessage, errorMessage, command, guid.ToString("D"));
    }

    public async Task GetWorkspaceAsAdminWithAccessDetails()
    {
        const string elementToGet = "workspace ID to get";

        Guid guid = GetEnteredGuid(elementToGet);

        string commandMessage = $"Get the {guid} workspace (admin) with access details";
        string successMessage = $"The {guid} workspace (admin) with access details";
        string errorMessage = $"getting the {guid} workspace (admin) with access details";

        async Task<IWorkspaceCommandResult<AdminWorkspace>> command(string worksapceId) => await adminWorkspacesService.GetWorkspaceDetails(worksapceId);

        await ExecuteCommand<IWorkspaceCommandResult<AdminWorkspace>, AdminWorkspace>(commandMessage, successMessage, errorMessage, command, guid.ToString("D"));
    }

    public async Task GetWorkspaceItems()
    {
        const string elementToGet = "workspace ID from which to get elements";

        Guid guid = GetEnteredGuid(elementToGet);

        string commandMessage = $"Get the {guid} workspace elements";
        string successMessage = $"The {guid} workspace elements";
        string errorMessage = $"getting the {guid} workspace elements";

        async Task<IWorkspaceCommandResult<FabricItemList>> command(string worksapceId) => await workspacesService.GetWorkspaceItems(worksapceId);

        await ExecuteCommand<IWorkspaceCommandResult<FabricItemList>, FabricItemList>(commandMessage, successMessage, errorMessage, command, guid.ToString("D"));
    }

    private async Task ExecuteCommand<CommandResultT, ResultT>(string commandMessage, string successMessage, string errorMessage, Func<Task<CommandResultT>> command)
        where CommandResultT : ICommandResult<ResultT>
        where ResultT : class
    {
        try
        {
            ConsoleHelper.WriteInfo($"{commandMessage} in the Fabric service");

            Stopwatch stopwatch = Stopwatch.StartNew();

            CommandResultT result = await command();

            stopwatch.Stop();
            ConsoleHelper.WriteVerbose($"Command duration: {stopwatch.Elapsed}");

            ConsoleHelper.WriteSuccess($"{successMessage} is avalable in the {result.ResultFilePath} file ({result.ResultCount} elements saved).");
        }
        catch (Exception error)
        {
            ConsoleHelper.WriteError($"An error occurred while {errorMessage} in the Fabric service", error);
        }
    }

    private async Task ExecuteCommand<CommandResultT, ResultT>(string commandMessage, string successMessage, string errorMessage, Func<string, Task<CommandResultT>> command, string argument)
        where CommandResultT : ICommandResult<ResultT>
        where ResultT : class
    {
        try
        {
            ConsoleHelper.WriteInfo($"{commandMessage} in the Fabric service");

            Stopwatch stopwatch = Stopwatch.StartNew();

            CommandResultT result = await command(argument);

            stopwatch.Stop();
            ConsoleHelper.WriteVerbose($"Command duration: {stopwatch.Elapsed}");

            ConsoleHelper.WriteSuccess($"{successMessage} is avalable in the {result.ResultFilePath} file ({result.ResultCount} elements saved).");
        }
        catch (Exception error)
        {
            ConsoleHelper.WriteError($"An error occurred while {errorMessage} in the Fabric service", error);
        }
    }

    private static Guid GetEnteredGuid(string elementToGet)
    {
        Guid guid = Guid.Empty;
        int retryCount = 0;
        const int maxRetryCount = 5;

        do
        {
            ConsoleHelper.WriteNewLine(1);

            ConsoleHelper.WriteInfo($"Enter the {elementToGet}:");
            ConsoleHelper.WriteNewLine(1);

            string enteredText = Console.ReadLine() ?? string.Empty;

            if (!Guid.TryParse(enteredText, out guid))
            {
                ConsoleHelper.WriteWarning("The entered text is no a valid GUID (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx, x, hexadecimal value: between 0 and 9 or A,B,C,D,E or F)");
                ConsoleHelper.WriteVerbose($"Attempt {++retryCount} / {maxRetryCount}");
            }

        } while (guid == Guid.Empty && retryCount < maxRetryCount);

        return guid;
    }
}
