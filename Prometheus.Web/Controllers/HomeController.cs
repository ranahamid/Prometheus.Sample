using Microsoft.AspNetCore.Mvc;
using Prometheus.Web.Models;
using System.Diagnostics;

namespace Prometheus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly Counter indexCounter = Metrics.CreateCounter("index_action", "some help about this");

        public HomeController(ILogger<HomeController> logger)
        {
            //var server = new MetricServer(8080);
            //server.Start();

            _logger = logger;
        }

        public IActionResult Index()
        {
            indexCounter.Inc();
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
}
