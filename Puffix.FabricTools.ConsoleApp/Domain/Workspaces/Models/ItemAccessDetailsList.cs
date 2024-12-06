using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class ItemAccessDetailsList
{
    [JsonPropertyName("accessDetails")]
    public ICollection<ItemAccessDetails> Elements { get; set; } = [];
}