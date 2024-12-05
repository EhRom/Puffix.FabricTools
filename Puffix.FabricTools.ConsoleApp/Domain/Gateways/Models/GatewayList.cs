using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Gateways.Models;

public class GatewayList : BaseFabricList<Gateway>
{
    [JsonPropertyName("value")]
    public override ICollection<Gateway> Elements { get; set; } = [];
}
