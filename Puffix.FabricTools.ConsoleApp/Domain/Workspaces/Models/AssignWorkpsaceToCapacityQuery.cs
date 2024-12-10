using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class AssignWorkpsaceToCapacityQuery
{
    [JsonPropertyName("capacityId")]
    public string CapacityId { get; set; } = string.Empty;


    [JsonPropertyName("workpsaceIdCollection")]
    public ICollection<string> WorkpsaceIdCollection { get; set; } = [];
}
