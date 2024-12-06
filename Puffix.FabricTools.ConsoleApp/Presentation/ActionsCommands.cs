using Puffix.ConsoleLogMagnifier;

namespace Puffix.FabricTools.ConsoleApp.Presentation;

public class ActionsCommands
{
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
            ConsoleHelper.Write("- R to assign roles from a file.");
            ConsoleHelper.Write("- Escape to return to main menu.");

            ConsoleHelper.WriteNewLine(1);
            key = ConsoleHelper.ReadKey();

            if (key == ConsoleKey.Escape)
                ConsoleHelper.WriteInfo("RReturn to main menu");
            else if (key == ConsoleKey.R)
                await AddRoleAssignments();
            //else if (key == ConsoleKey.C)
            //await ListCapcities();
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


    // TODO set user as admin (json format...)
    // TODO set workspace capacity > https://learn.microsoft.com/en-us/rest/api/fabric/core/workspaces/assign-to-capacity?tabs=HTTP
    // TODO https://learn.microsoft.com/en-us/rest/api/fabric/core/workspaces/unassign-from-capacity?tabs=HTTP

    // https://learn.microsoft.com/en-us/rest/api/fabric/articles/item-management/definitions/report-definition


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
}
