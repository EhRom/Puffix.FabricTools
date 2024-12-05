using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;

public abstract class BaseFabricList<ElementT>
    where ElementT : class
{
    // TODO v2
    // split classic vs list
    // rename with result
    // split models between exposed and internal

    // TODO in services:
    // manage continuation tokens in a base class.

    [JsonIgnore]
    public abstract ICollection<ElementT> Elements { get; set; }

    [JsonPropertyName("continuationUri")]
    public string ContinutationUri { get; set; } = string.Empty;

    [JsonPropertyName("continuationToken")]
    public string ContinutationToken { get; set; } = string.Empty;
}
