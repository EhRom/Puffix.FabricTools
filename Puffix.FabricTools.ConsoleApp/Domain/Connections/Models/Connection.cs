using System.Text.Json.Serialization;

namespace Puffix.FabricTools.ConsoleApp.Domain.Connections.Models;

public class Connection
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("connectivityType")]
    public string ConnectivityType { get; set; } = string.Empty;

    [JsonPropertyName("connectionDetails")]
    public ConnectionDetails ConnectionDetails { get; set; } = new();

    [JsonPropertyName("privacyLevel")]
    public string PrivacyLevel { get; set; } = string.Empty;

    [JsonPropertyName("credentialDetails")]
    public CredentialDetails CredentialDetails { get; set; } = new();
}

public class ConnectionDetails
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;
}

public class CredentialDetails
{
    [JsonPropertyName("credentialType")]
    public string CredentialType { get; set; } = string.Empty;

    [JsonPropertyName("singleSignOnType")]
    public string SingleSignOnType { get; set; } = string.Empty;

    [JsonPropertyName("connectionEncryption")]
    public string ConnectionEncryption { get; set; } = string.Empty;

    [JsonPropertyName("skipTestConnection")]
    public bool SkipTestConnection { get; set; } = false;
}