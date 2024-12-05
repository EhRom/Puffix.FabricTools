using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class FabricItemList : BaseFabricList<FabricItem>
{
    [JsonPropertyName("value")]
    public override ICollection<FabricItem> Elements { get; set; } = [];
}
