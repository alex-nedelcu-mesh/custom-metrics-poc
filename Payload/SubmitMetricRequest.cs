using System.Text.Json.Serialization;

public class SubmitMetricRequest
{
    [JsonPropertyName("series")]
    public required List<Series> Series { get; set; }
}