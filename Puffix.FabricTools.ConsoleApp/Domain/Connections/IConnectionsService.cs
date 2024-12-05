using Puffix.FabricTools.ConsoleApp.Domain.Connections.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Connections;

public interface IConnectionsService
{
    Task<IConnectionCommandResult<ConnectionList>> List();
}
