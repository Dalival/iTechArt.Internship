using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.Foundation.Result;
using ITechArt.Surveys.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITechArt.Surveys.WebApp.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserService _userService;


        public RegistrationController(IUserService userService)
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

            if (model.Password != model.PasswordConfirmation)
            {
                ModelState.AddModelError(nameof(model.PasswordConfirmation),
                    "The password and the confirmation password do not match.");

                return View(model);
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userService.CreateUserAsync(user, model.Password, model.PasswordConfirmation);
            if (!result.Succeeded)
            {
                AddAuthErrors(result.Errors);

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }


        private void AddAuthErrors(IEnumerable<RegistrationError> errors)
        {
            foreach (var error in errors)
            {
                (string key, string message) modelError = error switch
                {
                    RegistrationError.UnknownError => (string.Empty,
                        "Something went wrong. Try to enter another data."),
                    RegistrationError.InvalidUserName => (nameof(RegistrationViewModel.UserName),
                        "Invalid username. Allowed symbols: -._@+"),
                    RegistrationError.DuplicateUserName => (nameof(RegistrationViewModel.UserName),
                        "User with such username already exists"),
                    RegistrationError.InvalidEmail => (nameof(RegistrationViewModel.Email),
                        "Invalid email"),
                    RegistrationError.DuplicateEmail => (nameof(RegistrationViewModel.Email),
                        "User with such email already exists"),
                    RegistrationError.PasswordTooShort => (nameof(RegistrationViewModel.Password),
                        "Password should contain 6-30 characters"),
                    RegistrationError.PasswordRequiresDigit => (nameof(RegistrationViewModel.Password),
                        "Password should contain at least one digit"),
                    RegistrationError.PasswordRequiresLower => (nameof(RegistrationViewModel.Password),
                        "Password should contain at least one lowercase letter"),
                    RegistrationError.PasswordRequiresUpper => (nameof(RegistrationViewModel.Password),
                        "Password should contain at least one uppercase letter"),
                    RegistrationError.PasswordRequiresMoreUniqueChars => (nameof(RegistrationViewModel.Password),
                        "Please add more unique symbols"),
                    RegistrationError.PasswordRequiresNonAlphanumeric => (nameof(RegistrationViewModel.Password),
                        "Password should contain at least one non alphanumeric symbol. Please add one of these: -._@+"),
                    _ => throw new ArgumentOutOfRangeException(error.ToString())
                };

                ModelState.AddModelError(modelError.key, modelError.message);
            }
        }
    }
}
