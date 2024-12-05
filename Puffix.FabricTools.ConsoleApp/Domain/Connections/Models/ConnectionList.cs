using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Connections.Models;

public class ConnectionList : BaseFabricList<Connection>
{
    [JsonPropertyName("value")]
    public override ICollection<Connection> Elements { get; set; } = [];
}
