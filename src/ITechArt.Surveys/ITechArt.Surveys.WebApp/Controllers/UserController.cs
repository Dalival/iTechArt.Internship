using ITechArt.Surveys.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITechArt.Surveys.WebApp.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public IActionResult Login()
        {
            var model = new RegistrationViewModel();

            return View(model);
        }
    }
}
