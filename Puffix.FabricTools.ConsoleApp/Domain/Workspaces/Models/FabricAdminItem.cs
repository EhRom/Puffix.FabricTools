using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class FabricAdminItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string ItemType { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("lastUpdatedDate")]
    public DateTime LastUpdatedDate { get; set; } = DateTime.MinValue;

    [JsonPropertyName("workspaceId")]
    public string WorkspaceId { get; set; } = string.Empty;

    [JsonPropertyName("capacityId")]
    public string CapacityId { get; set; } = string.Empty;

    [JsonPropertyName("creatorPrincipal")]
    public Principal Creator { get; set; } = new();

    [JsonPropertyName("accessDetails")]
    public ICollection<ItemAccessDetails> AccessDetails { get; set; } = [];
}