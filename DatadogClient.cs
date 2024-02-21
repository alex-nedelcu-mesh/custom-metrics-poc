using System.Text.Json;


public class DatadogClient
{

    private readonly HttpClient _httpClient;

    public DatadogClient()
    {
        _httpClient = new HttpClient();
    }

    public async Task SubmitMetric(string metric, int? value)
    {
        if (metric == null || value == null)
        {
            return;
        }

        var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.DatadogMetrics);

        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("DD-API-KEY", Environment.GetEnvironmentVariable("DD_API_KEY"));
        request.Headers.Add("DD-APPLICATION-KEY", Environment.GetEnvironmentVariable("DD_APP_KEY"));

        var submitMetricRequest = new SubmitMetricRequest
        {
            Series =
           [
               new Series
                {
                    Metric = metric,
                    Points =
                    [
                        new Point
                        {
                            Timestamp =  ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds(),
                            Value = (int)value
                        }
                    ],
                    Type = MetricType.Count,
                    Interval = 1
                }
           ]
        };

        var content = new StringContent(JsonSerializer.Serialize(submitMetricRequest), null, "application/json");

        request.Content = content;
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}