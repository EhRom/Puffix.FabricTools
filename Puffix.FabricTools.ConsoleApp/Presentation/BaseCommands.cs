using Puffix.ConsoleLogMagnifier;
using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using System.Diagnostics;

namespace Puffix.FabricTools.ConsoleApp.Presentation;

public static class BaseCommands
{
    public static async Task ExecuteCommand<CommandResultT, ResultT>(string commandMessage, string successMessage, string errorMessage, Func<Task<CommandResultT>> command)
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

    public static async Task ExecuteCommand<CommandResultT, ResultT>(string commandMessage, string successMessage, string errorMessage, Func<string, Task<CommandResultT>> command, string argument)
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

    public static async Task ExecuteCommand<CommandResultT, ResultT>(string commandMessage, string successMessage, string errorMessage, Func<string, string, Task<CommandResultT>> command, string firstArgument, string seceondArgument)
        where CommandResultT : ICommandResult<ResultT>
        where ResultT : class
    {
        try
        {
            ConsoleHelper.WriteInfo($"{commandMessage} in the Fabric service");

            Stopwatch stopwatch = Stopwatch.StartNew();

            CommandResultT result = await command(firstArgument, seceondArgument);

            stopwatch.Stop();
            ConsoleHelper.WriteVerbose($"Command duration: {stopwatch.Elapsed}");

            ConsoleHelper.WriteSuccess($"{successMessage} is avalable in the {result.ResultFilePath} file ({result.ResultCount} elements saved).");
        }
        catch (Exception error)
        {
            ConsoleHelper.WriteError($"An error occurred while {errorMessage} in the Fabric service", error);
        }
    }

    public static Guid GetEnteredGuid(string elementToGet)
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

    public static string GetEnteredPath(string elementToGet)
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
}
