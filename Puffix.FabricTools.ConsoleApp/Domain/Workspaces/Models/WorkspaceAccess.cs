using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class WorkspaceAccess
{
    [JsonPropertyName("principal")]
    public Principal Principal { get; set; } = new();

    [JsonPropertyName("workspaceAccessDetails")]
    public WorkspaceAccessDetails WorkspaceAccessDetails { get; set; } = new();
}

public class WorkspaceAccessDetails
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("workspaceRole")]
    public string WorkspaceRole { get; set; } = string.Empty;

}
