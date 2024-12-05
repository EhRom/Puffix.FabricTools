using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class RoleAssignementList : BaseFabricList<RoleAssignement>
{
    [JsonPropertyName("value")]
    public override ICollection<RoleAssignement> Elements { get; set; } = [];
}
