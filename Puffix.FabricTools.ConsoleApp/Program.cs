using Microsoft.Extensions.Configuration;
using Puffix.ConsoleLogMagnifier;
using Puffix.FabricTools.ConsoleApp.Infra;
using Puffix.FabricTools.ConsoleApp.Presentation;
using Puffix.IoC.Configuration;

ConsoleHelper.WriteInfo("Welcome to the Fabric API Console");

IIoCContainerWithConfiguration container;

try
{
    ConsoleHelper.WriteVerbose("Initialize application.");
    IConfiguration configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile("appSettings.local.json", optional: true, reloadOnChange: true)
       .Build();

    container = IoCContainer.BuildContainer(configuration);
}
catch (Exception error)
{
    ConsoleHelper.WriteError("An error occured while initilizing the application.", error);
    return;
}

ConsoleKey key;

do
{
    ConsoleHelper.WriteNewLine(2);
    ConsoleHelper.Write("Press:");
    ConsoleHelper.Write("- A to navigate to authentication menu (mandatory for other actions).");
    ConsoleHelper.Write("- I to navigate to the inventory menu.");
    ConsoleHelper.Write("- C to navigate to the action menu.");
    ConsoleHelper.Write("- Q to quit.");

    ConsoleHelper.WriteNewLine(1);
    key = ConsoleHelper.ReadKey();

    if (key == ConsoleKey.Q)
        ConsoleHelper.WriteInfo("Thank you for using the  Fabric API console App. See you soon!");
    else if (key == ConsoleKey.A)
    {
        AuthenticationCommands authenticationCommands = container.Resolve<AuthenticationCommands>();
        await authenticationCommands.SelectAuthenticationCommand();
    }
    else if (key == ConsoleKey.I)
    {
        InventoryCommands inventoryCommands = container.Resolve<InventoryCommands>();
        await inventoryCommands.SelectInventoryCommand();
    }
    else if (key == ConsoleKey.C)
    {
        ActionsCommands authenticationCommands = container.Resolve<ActionsCommands>();
        await authenticationCommands.SelectActionCommand();
    }
    else
        ConsoleHelper.WriteWarning($"The key {key} is not a known command (for the moment :-) )");

} while (key != ConsoleKey.Q);