using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation;
using ITechArt.Surveys.Repositories;
using ITechArt.Surveys.WebApp.Models;

namespace ITechArt.Surveys.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CounterService _counterService;
        
        public HomeController(ILogger<HomeController> logger,
            CounterService counterService)
        {
            _logger = logger;
            _counterService = counterService;
        }

        public IActionResult Index()
        {
            var model = new CounterViewModel()
            {
                Counter = _counterService.IncrementAndGetCounter().Value
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
