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
        private readonly UnitOfWork _unitOfWork;
        
        public HomeController(ILogger<HomeController> logger,
            UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var counter = new Counter();
            if (!_unitOfWork.Counters.GetAll().Any())
            {
                _unitOfWork.Counters.Save(counter);
            }
            counter = _unitOfWork.Counters.GetAll().First();
            counter.Value++;
            var model = new CounterViewModel
            {
                Counter = counter.Value
            };
            _unitOfWork.Counters.Save(counter);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
