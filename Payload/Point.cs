using System.Text.Json.Serialization;

public class Point
{
    [JsonPropertyName("timestamp")]
    public required long Timestamp { get; set; }

    [JsonPropertyName("value")]
    public required int Value { get; set; }
}