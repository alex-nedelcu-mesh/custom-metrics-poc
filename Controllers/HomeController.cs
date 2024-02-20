using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using custom_metrics_poc.Models;

namespace custom_metrics_poc.Controllers;

public class HomeController : Controller
{
    private readonly DatadogClient _datadogClient;

    public HomeController()
    {
        _datadogClient = new DatadogClient();
    }

    [HttpGet()]
    public async Task<string> Index([FromQuery(Name = "value")] int? value)
    {

        if (value == null)
        {
            return "Don't forget to include 'value' query param";
        }
        else
        {
            await _datadogClient.SubmitMetric("custom.metric.test", value);
            return "Custom metric sent!";
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
