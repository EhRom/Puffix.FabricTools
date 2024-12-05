using Puffix.FabricTools.ConsoleApp.Domain.Gateways.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Gateways;

public interface IGatewaysService
{
    Task<IGatewayCommandResult<GatewayList>> List();
}
