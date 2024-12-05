using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Domains.Models;

public class FabricDomain
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("parentDomainId")]
    public string ParentDomainId { get; set; } = string.Empty;

    [JsonPropertyName("contributorsScope")]
    public string ContributorsScope { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{base.ToString()}--{Id}-{DisplayName}-{ContributorsScope}";
    }
}
