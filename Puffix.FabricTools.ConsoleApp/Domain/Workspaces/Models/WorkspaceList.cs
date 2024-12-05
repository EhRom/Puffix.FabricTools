using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class WorkspaceList : BaseFabricList<Workspace>
{
    [JsonPropertyName("value")]
    public override ICollection<Workspace> Elements { get; set; } = [];
}
