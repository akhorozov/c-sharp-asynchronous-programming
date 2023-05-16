using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalyzer.Core.Domain;
using StockAnalyzer.Web.Models;
using System.Diagnostics;

namespace StockAnalyzer.Web.Controllers;

public class HomeController : Controller
{
    private static string API_URL = "https://ps-async.fekberg.com/api/stocks";

    public async Task<IActionResult> Index()
    {
        using (HttpClient client = new HttpClient())
        {
            Task<HttpResponseMessage> responseTask = client.GetAsync($"{API_URL}/MSFT");
            HttpResponseMessage response = await responseTask;

            string content = await response.Content.ReadAsStringAsync();

            IEnumerable<StockPrice>? data = JsonConvert.DeserializeObject<IEnumerable<StockPrice>>(content);
        }

        return View();
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