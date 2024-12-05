namespace Puffix.FabricTools.ConsoleApp.Domain.Token;

public interface ITokenRestApiService
{
    Task<DateTimeOffset> GetToken();

    Task<bool> LoadToken();

    Task SaveToken();

    Task EraseToken();

    Task SwitchAuthenticationMode();
}
