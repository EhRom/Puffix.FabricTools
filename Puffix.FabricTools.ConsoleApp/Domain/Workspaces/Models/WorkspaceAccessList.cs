using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class WorkspaceAccessList : BaseFabricList<WorkspaceAccess>
{
    [JsonPropertyName("accessDetails")]
    public override ICollection<WorkspaceAccess> Elements { get; set; } = [];
}
