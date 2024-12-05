using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class AdminWorkspaceList : BaseFabricList<AdminWorkspace>
{
    [JsonPropertyName("workspaces")]
    public override ICollection<AdminWorkspace> Elements { get; set; } = [];
}
