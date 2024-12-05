using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class GitConnection
{
    [JsonPropertyName("workspaceId")]
    public string WorkspaceId { get; set; } = string.Empty;

    [JsonPropertyName("gitProviderDetails")]
    public GitProviderDetails GitProviderDetails { get; set; } = new();
}

public class GitProviderDetails
{
    [JsonPropertyName("ownerName")]
    public string OwnerName { get; set; } = string.Empty;

    [JsonPropertyName("gitProviderType")]
    public string GitProviderType { get; set; } = string.Empty;

    [JsonPropertyName("repositoryName")]
    public string RepositoryName { get; set; } = string.Empty;

    [JsonPropertyName("branchName")]
    public string BranchName { get; set; } = string.Empty;

    [JsonPropertyName("directoryName")]
    public string DirectoryName { get; set; } = string.Empty;
}