using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Gateways.Models;

public class Gateway
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("capacityId")]
    public string CapacityId { get; set; } = string.Empty;

    [JsonPropertyName("virtualNetworkAzureResource")]
    public VirtualNetworkAzureResource VirtualNetworkAzureResource { get; set; } = new();

    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    [JsonPropertyName("inactivityMinutesBeforeSleep")]
    public int InactivityMinutesBeforeSleep { get; set; } = 0;

    [JsonPropertyName("numberOfMemberGateways")]
    public int NumberOfMemberGateways { get; set; } = 0;

    [JsonPropertyName("loadBalancingSetting")]
    public string LoadBalancingSetting { get; set; } = string.Empty;

    [JsonPropertyName("allowCloudConnectionRefresh")]
    public bool AllowCloudConnectionRefresh { get; set; } = false;

    [JsonPropertyName("allowCustomConnectors")]
    public bool AllowCustomConnectors { get; set; } = false;

}

public class VirtualNetworkAzureResource
{
    [JsonPropertyName("subscriptionId")]
    public string SubscriptionId { get; set; } = string.Empty;

    [JsonPropertyName("resourceGroupName")]
    public string ResourceGroupName { get; set; } = string.Empty;

    [JsonPropertyName("virtualNetworkName")]
    public string VirtualNetworkName { get; set; } = string.Empty;

    [JsonPropertyName("subnetName")]
    public string SubnetName { get; set; } = string.Empty;
}