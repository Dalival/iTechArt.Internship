using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using ITechArt.Common.Logger;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.WebApp.Models;

namespace ITechArt.Surveys.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICounterService _counterService;


        public HomeController(ICounterService counterService)
        {
            _counterService = counterService;
        }


        public async Task<IActionResult> Index()
        {
            var counterFromDatabase = await _counterService.IncrementAndGetCounter();
            var model = new CounterViewModel()
            {
                Value = counterFromDatabase.Value
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
