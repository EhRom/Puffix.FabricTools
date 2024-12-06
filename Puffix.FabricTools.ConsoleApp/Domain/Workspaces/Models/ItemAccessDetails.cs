using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class ItemAccessDetails
{
    public Principal Principal { get; set; } = new();

    public ItemAccessDetailsInfo AccessDetails { get; set; } = new();
}

public class ItemAccessDetailsInfo
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("permissions")]
    public ICollection<string> Permissions { get; set; } = [];

    [JsonPropertyName("id")]
    public ICollection<string> AdditionalPermissions { get; set; } = [];
}