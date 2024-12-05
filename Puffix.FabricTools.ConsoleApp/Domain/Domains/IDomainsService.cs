using Puffix.FabricTools.ConsoleApp.Domain.Domains.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Domains;

public interface IDomainsService
{
    Task<IDomainCommandResult<FabricDomainList>> List();
}
