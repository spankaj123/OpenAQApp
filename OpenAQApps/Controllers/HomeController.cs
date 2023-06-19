using APILibrary;
using Microsoft.AspNetCore.Mvc;
using OpenAQApps.Models;
using System.Diagnostics;

namespace OpenAQApps.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IOpenAQProcessor _openAQProcessor;

        public HomeController(ILogger<HomeController> logger, IOpenAQProcessor openAQProcessor)
        {
            _logger = logger;
            _openAQProcessor = openAQProcessor;
            ApiHelper.InitializeClient();
        }
        
        public IActionResult Index()
        {
            try
            {
                //Passing api params through dictionary. Currently haordcoded but we can make changes in params based on inputs from UI if required
                Dictionary<string, string> apiParams = new Dictionary<string, string>();
                apiParams.Add("limit", "100");
                apiParams.Add("page", "1");
                apiParams.Add("sort", "desc");
                apiParams.Add("radius", "1000");
                apiParams.Add("order_by", "lastUpdated");
                apiParams.Add("dumpRaw", "false");
                _logger.LogInformation("Sending request to AQ API for results with api parameters", apiParams);
                var result = _openAQProcessor.LoadAQResults(apiParams);
                    return View(result.Result);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting AQ data", ex.Message);
                return View(new NotFoundResult());
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
}