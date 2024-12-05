using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Domains.Models;

public class FabricDomainList : BaseFabricList<FabricDomain>
{
    [JsonPropertyName("domains")]
    public override ICollection<FabricDomain> Elements { get; set; } = [];
}
