using Puffix.ConsoleLogMagnifier;
using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using Puffix.FabricTools.ConsoleApp.Domain.Workspaces;
using Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;
using System.Diagnostics;

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
            ConsoleHelper.Write("- A to assign a workspace list to a capacity.");
            ConsoleHelper.Write("- U to unassign a workspace list from a capacity.");
            ConsoleHelper.Write("- Escape to return to main menu.");

            ConsoleHelper.WriteNewLine(1);
            key = ConsoleHelper.ReadKey();

            if (key == ConsoleKey.Escape)
                ConsoleHelper.WriteInfo("Return to main menu");
            //else if (key == ConsoleKey.R)
            //    await AddRoleAssignments();
            else if (key == ConsoleKey.A)
                await AssignWorkspaceCollectionToCapacity();
            else if (key == ConsoleKey.U)
                await UnassignWorkspaceCollectionToCapacity();
            else
                ConsoleHelper.WriteWarning($"The key {key} is not a known command (for the moment :-) )");

        } while (key != ConsoleKey.Escape);
    }

    public async Task AddRoleAssignments()
    {
        const string elementToGet = "role assignements configuration";

        string path = GetEnteredPath(elementToGet);

        await Task.Delay(100);
    }

    // TODO set user as admin (json format...)
    // https://learn.microsoft.com/en-us/rest/api/fabric/core/workspaces/update-workspace-role-assignment?tabs=HTTP
    // https://learn.microsoft.com/en-us/rest/api/fabric/core/workspaces/provision-identity?tabs=HTTP
    // https://learn.microsoft.com/en-us/rest/api/fabric/core/workspaces/list-workspace-role-assignments?tabs=HTTP

    public async Task AssignWorkspaceCollectionToCapacity()
    {
        const string elementToGet = "capacity and workspace collection";
        string configurationFilePath = GetEnteredPath(elementToGet);

        if (string.IsNullOrEmpty(configurationFilePath ))
        {
            ConsoleHelper.WriteError("A valid file path is required to assign a workspace list to a capacity.");
            return;
        }

        string commandMessage = $"Assign a list of workspace to a capacity";
        string successMessage = $"The workspaces are assgined to the capacity. The updated workspace collection";
        string errorMessage = $"assigning a list of workspace to a capacity";

        async Task<IWorkspaceCommandResult<WorkspaceList>> command(string configurationFilePath) => await workspacesService.AssignWorkspaceCollectionToCapacity(configurationFilePath);

        await ExecuteCommand<IWorkspaceCommandResult<WorkspaceList>, WorkspaceList>(commandMessage, successMessage, errorMessage, command, configurationFilePath);
    }

    public async Task UnassignWorkspaceCollectionToCapacity()
    {
        const string elementToGet = "capacity and workspace collection";
        string configurationFilePath = GetEnteredPath(elementToGet);

        if (string.IsNullOrEmpty(configurationFilePath))
        {
            ConsoleHelper.WriteError("A valid file path is required to unassign a workspace list from a capacity.");
            return;
        }

        string commandMessage = $"Unssign a list of workspace from a capacity";
        string successMessage = $"The workspaces are unassgined from the capacity. The updated workspace collection";
        string errorMessage = $"unassigning a list of workspace from a capacity";

        async Task<IWorkspaceCommandResult<WorkspaceList>> command(string configurationFilePath) => await workspacesService.UnassignWorkspaceCollectionToCapacity(configurationFilePath);

        await ExecuteCommand<IWorkspaceCommandResult<WorkspaceList>, WorkspaceList>(commandMessage, successMessage, errorMessage, command, configurationFilePath);
    }

    private static string GetEnteredPath(string elementToGet)
    {
        string path = string.Empty;
        int retryCount = 0;
        const int maxRetryCount = 5;

        do
        {
            ConsoleHelper.WriteNewLine(1);

            ConsoleHelper.WriteInfo($"Enter the {elementToGet} file path:");
            ConsoleHelper.WriteNewLine(1);

            string enteredText = Console.ReadLine() ?? string.Empty;
            ConsoleHelper.ClearLastLines();

            if (!Path.Exists(enteredText))
            {
                ConsoleHelper.WriteWarning("The entered text is no a valid path, or the file or directory does not exists");
                ConsoleHelper.WriteVerbose($"Attempt {++retryCount} / {maxRetryCount}");
            }
            else
                path = enteredText;

        } while (path == string.Empty && retryCount < maxRetryCount);

        return path;
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
            ConsoleHelper.ClearLastLines();

            if (!Guid.TryParse(enteredText, out guid))
            {
                ConsoleHelper.WriteWarning("The entered text is no a valid GUID (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx, x, hexadecimal value: between 0 and 9 or A,B,C,D,E or F)");
                ConsoleHelper.WriteVerbose($"Attempt {++retryCount} / {maxRetryCount}");
            }

        } while (guid == Guid.Empty && retryCount < maxRetryCount);

        return guid;
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
}
