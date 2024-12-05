using Puffix.FabricTools.ConsoleApp.Domain.Capacities.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Capacities;

public interface ICapacitiesService
{
    Task<ICapacityCommandResult<CapacityList>> List();
}
