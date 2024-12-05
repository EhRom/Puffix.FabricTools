using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

public class GitConnectionList : BaseFabricList<GitConnection>
{
    [JsonPropertyName("value")]
    public override ICollection<GitConnection> Elements { get; set; } = [];
}
