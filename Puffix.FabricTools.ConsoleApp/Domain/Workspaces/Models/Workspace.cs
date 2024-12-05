using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class Workspace
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string WorkspaceType { get; set; } = string.Empty;

    [JsonPropertyName("capacityId")]
    public string CapacityId { get; set; } = string.Empty;

    [JsonPropertyName("capacityAssignmentProgress")]
    public string CapacityAssignmentProgress { get; set; } = string.Empty;

    [JsonPropertyName("workspaceIdentity")]
    public WorkspaceIdentity WorkspaceIdentity { get; set; } = new WorkspaceIdentity();

    [JsonPropertyName("capacityRegion")]
    public string CapacityRegion { get; set; } = string.Empty;

    [JsonPropertyName("oneLakeEndpoints")]
    public WorkspaceOneLakeEndpoints OneLakeEndpoints { get; set; } = new WorkspaceOneLakeEndpoints();

    [JsonPropertyName("roleAssignements")]
    public RoleAssignementList RoleAssignements { get; set; } = new RoleAssignementList();

    public override string ToString()
    {
        return $"{base.ToString()}--{Id}-{DisplayName}-{WorkspaceType}-{CapacityId}";
    }
}

public class WorkspaceIdentity
{
    [JsonPropertyName("applicationId")]
    public string ApplicationId { get; set; } = string.Empty;

    [JsonPropertyName("capacityId")]
    public string ServicePrincipalId { get; set; } = string.Empty;
}

public class WorkspaceOneLakeEndpoints
{
    [JsonPropertyName("blobEndpoint")]
    public string BlobEndpoint { get; set; } = string.Empty;

    [JsonPropertyName("dfsEndpoint")]
    public string DfsEndpoint { get; set; } = string.Empty;
}
