using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class FabricAdminItemList : BaseFabricList<FabricAdminItem>
{
    [JsonPropertyName("itemEntities")]
    public override ICollection<FabricAdminItem> Elements { get; set; } = [];
}