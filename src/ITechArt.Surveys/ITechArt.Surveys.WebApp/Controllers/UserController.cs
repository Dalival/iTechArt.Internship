using System.Threading.Tasks;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITechArt.Surveys.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            var model = new RegistrationViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _userService.IsEmailExistAsync(model.Email))
            {
                ModelState.AddModelError(nameof(RegistrationViewModel.Email), "Пользователь с такой почтой уже существует");

                return View(model);
            }

            if (await _userService.IsUserNameExistAsync(model.UserName))
            {
                ModelState.AddModelError(nameof(RegistrationViewModel.UserName), "Пользователь с таким именем уже существует");

                return View(model);
            }

            await _userService.CreateUserAsync(model.UserName, model.Email, model.Password);

            return RedirectToAction("Index", "Home");
        }
    }
}
