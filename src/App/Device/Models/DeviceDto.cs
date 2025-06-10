using System.Text.Json.Serialization;

namespace LodgerBackend.Device.Models;

public class DeviceDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("location")]
    public required string Location { get; set; }

    [JsonPropertyName("date")]
    public required string Date { get; set; }

}