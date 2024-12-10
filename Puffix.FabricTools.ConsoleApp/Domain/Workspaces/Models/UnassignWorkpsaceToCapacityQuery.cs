using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class UnassignWorkpsaceToCapacityQuery
{
    [JsonPropertyName("workpsaceIdCollection")]
    public ICollection<string> WorkpsaceIdCollection { get; set; } = [];
}
