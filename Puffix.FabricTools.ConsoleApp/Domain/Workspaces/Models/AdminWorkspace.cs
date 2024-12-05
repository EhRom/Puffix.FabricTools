using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class AdminWorkspace
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string WorkspaceType { get; set; } = string.Empty;

    [JsonPropertyName("capacityId")]
    public string CapacityId { get; set; } = string.Empty;

    [JsonPropertyName("accessDetails")]
    public ICollection<WorkspaceAccess> AccessDetails { get; set; } = [];

    public override string ToString()
    {
        return $"{base.ToString()}--{Id}-{Name}-{WorkspaceType}-{CapacityId}";
    }
}
