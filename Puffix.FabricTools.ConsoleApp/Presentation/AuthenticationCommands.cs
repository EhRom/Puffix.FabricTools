using Puffix.ConsoleLogMagnifier;
using Puffix.FabricTools.ConsoleApp.Domain.Token;

namespace Puffix.FabricTools.ConsoleApp.Presentation;

public class AuthenticationCommands(ITokenRestApiService tokenRestApiService)
{
    private readonly ITokenRestApiService tokenRestApiService = tokenRestApiService;

    // TODO sub menu
    // - Automatically save toke (parameter)
    // - Switch authentication mode

    public async Task SelectAuthenticationCommand()
    {
        ConsoleKey key;

        do
        {
            ConsoleHelper.WriteNewLine(2);
            ConsoleHelper.WriteInfo("Authentication menu.");
            ConsoleHelper.WriteNewLine(1);
            ConsoleHelper.Write("Press:");
            ConsoleHelper.Write("- A to authenticate to the Fabric service (mandatory for other actions).");
            ConsoleHelper.Write("- L to load token from configuration, if valid.");
            ConsoleHelper.Write("- S to save the token.");
            ConsoleHelper.Write("- E to erase the saved token.");
            ConsoleHelper.Write("- M to swith the authentication mode (erase the saved token).");
            ConsoleHelper.Write("- Escape to return to main menu.");

            ConsoleHelper.WriteNewLine(1);

            key = Console.ReadKey().Key;

            ConsoleHelper.ClearLastCharacters(1);

            if (key == ConsoleKey.Escape)
                ConsoleHelper.WriteInfo("RReturn to main menu");
            else if (key == ConsoleKey.A)
                await Authenticate();
            else if (key == ConsoleKey.L)
                await LoadToken();
            else if (key == ConsoleKey.S)
                await SaveToken();
            else if (key == ConsoleKey.E)
                await EraseToken();
            else if (key == ConsoleKey.M)
                ConsoleHelper.WriteWarning("Under construction.");
            else
                ConsoleHelper.WriteWarning($"The key {key} is not a known command (for the moment :-) )");

        } while (key != ConsoleKey.Escape);
    }

    public async Task Authenticate()
    {
        try
        {
            ConsoleHelper.WriteInfo("Authenticate to the Fabric service");

            DateTimeOffset expirationDate = await tokenRestApiService.GetToken();

            ConsoleHelper.WriteSuccess($"The authentication token is valid until {expirationDate}");
        }
        catch (Exception error)
        {
            ConsoleHelper.WriteError("An error occurred while authenticating to the Fabric service", error);
        }
    }

    public async Task LoadToken()
    {
        try
        {
            ConsoleHelper.WriteInfo("Load the Fabric service token from the local configuration");

            bool isValidToken = await tokenRestApiService.LoadToken();

            if (isValidToken)
                ConsoleHelper.WriteSuccess("The token is still valid and available in cache");
            else
                ConsoleHelper.Write(ConsoleColor.Magenta, "A new token is required.");
        }
        catch (Exception error)
        {
            ConsoleHelper.WriteError("An error occurred while loading the Fabric service token from the local configuration", error);
        }
    }

    public async Task SaveToken()
    {
        try
        {
            ConsoleHelper.WriteInfo("Save the Fabric service token in the local configuration");

            await tokenRestApiService.SaveToken();

            ConsoleHelper.WriteSuccess($"The token has been saved in the local configuration");
        }
        catch (Exception error)
        {
            ConsoleHelper.WriteError("An error occurred while saving the Fabric service token in the local configuration", error);
        }
    }

    public async Task EraseToken()
    {
        try
        {
            ConsoleHelper.WriteInfo("Erase the Fabric service token in the local configuration");

            await tokenRestApiService.EraseToken();

            ConsoleHelper.WriteSuccess($"The token has been deleted from the local configuration");
        }
        catch (Exception error)
        {
            ConsoleHelper.WriteError("An error occurred while erasing the Fabric service token in the local configuration", error);
        }
    }
}
