using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Repositories;
using ITechArt.Surveys.WebApp.Models;

namespace ITechArt.Surveys.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UnitOfWork _repository;
        
        public HomeController(ILogger<HomeController> logger,
            UnitOfWork repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var counter = _repository.Counters.GetAll().First();
            counter.Value++;
            var model = new CounterViewModel
            {
                Counter = counter.Value
            };
            _repository.Counters.Save(counter);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
