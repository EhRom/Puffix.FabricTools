using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Capacities.Models;

public class CapacityList : BaseFabricList<Capacity>
{
    [JsonPropertyName("value")]
    public override ICollection<Capacity> Elements { get; set; } = [];
}