using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class CapacityQueryContent()
{
    [JsonPropertyName("capacityId")]
    public string CapacityId { get; set; } = string.Empty;

    public static CapacityQueryContent CreateNew(string capacityId)
    {
        return new CapacityQueryContent
        {
            CapacityId = capacityId
        };
    }
}
