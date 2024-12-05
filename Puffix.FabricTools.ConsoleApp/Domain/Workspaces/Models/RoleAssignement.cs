using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class RoleAssignement
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("principal")]
    public Principal Principal { get; set; } = new Principal();

    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;
}