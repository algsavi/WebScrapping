using AeC.WebScrapping.Domain;
using AeC.WebScrapping.Domain.Interfaces;
using AeC.WebScrapping.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AeC.WebScrapping.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScrappingService _scrappingService;

        public HomeController(ILogger<HomeController> logger, IScrappingService scrappingService)
        {
            _logger = logger;
            _scrappingService = scrappingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Extract(string json)
        {
            if (ModelState.IsValid)
            {
                _scrappingService.ScrapeAsync(json);

                return View("Index", json);
            }

            return View(json);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
