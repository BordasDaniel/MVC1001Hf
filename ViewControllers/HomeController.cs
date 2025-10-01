using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB_App__MVC_.Models;

namespace WEB_App__MVC_.ViewControllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sportolo()
        {
            List<Sportolo> sportolokLista = new SportoloController().GetSporterFromDatabase();
            return View(sportolokLista);
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
