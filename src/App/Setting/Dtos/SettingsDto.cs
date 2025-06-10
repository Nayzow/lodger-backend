using System.Text.Json.Serialization;

namespace LodgerBackend.Setting.Dtos;

public class SettingsDto
{
    [JsonPropertyName("push")]
    public bool Push { get; set; }

    [JsonPropertyName("email")]
    public bool Email { get; set; }

    [JsonPropertyName("sms")]
    public bool Sms { get; set; }

    [JsonPropertyName("folderActivity")]
    public bool FolderActivity { get; set; }

    [JsonPropertyName("listingUpdates")]
    public bool ListingUpdates { get; set; }

    [JsonPropertyName("accountSecurity")]
    public bool AccountSecurity { get; set; }
}