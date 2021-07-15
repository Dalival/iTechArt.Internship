﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.WebApp.Models;

namespace ITechArt.Surveys.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICounterService _counterService;


        public HomeController(ILogger<HomeController> logger,
            ICounterService counterService)
        {
            _logger = logger;
            _counterService = counterService;
        }


        public async Task<IActionResult> Index()
        {
            var counterFromDatabase = await _counterService.IncrementAndGetCounter();
            var model = new CounterViewModel()
            {
                CounterValue = counterFromDatabase.Value
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
