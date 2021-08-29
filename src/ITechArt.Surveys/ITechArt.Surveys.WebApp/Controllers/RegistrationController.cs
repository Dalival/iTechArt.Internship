using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation;
using ITechArt.Surveys.Foundation.Interfaces;
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

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(nameof(RegistrationViewModel.ConfirmPassword),
                    "The password and the confirmation password do not match.");
                return View(model);
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var errors = await _userService.CreateUserAsync(user, model.Password);
            if (errors.Any())
            {
                AddAuthErrors(errors);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }


        private void AddAuthErrors(IEnumerable<AuthError> errors)
        {
            foreach (var error in errors)
            {
                switch (error)
                {
                    case AuthError.DefaultError:
                        ModelState.AddModelError(string.Empty,
                            "Something went wrong. Try to enter another data.");
                        break;
                    case AuthError.InvalidUserName:
                        ModelState.AddModelError(nameof(RegistrationViewModel.UserName),
                            "Invalid username. Allowed symbols: -._@+");
                        break;
                    case AuthError.DuplicateUserName:
                        ModelState.AddModelError(nameof(RegistrationViewModel.UserName),
                            "User with such username already exists");
                        break;
                    case AuthError.InvalidEmail:
                        ModelState.AddModelError(nameof(RegistrationViewModel.Email),
                            "Invalid email");
                        break;
                    case AuthError.DuplicateEmail:
                        ModelState.AddModelError(nameof(RegistrationViewModel.Email),
                            "User with such email already exists");
                        break;
                    case AuthError.PasswordTooShort:
                        ModelState.AddModelError(nameof(RegistrationViewModel.Password),
                            "Password should contain 6-30 characters");
                        break;
                    case AuthError.PasswordRequiresDigit:
                        ModelState.AddModelError(nameof(RegistrationViewModel.Password),
                            "Password should contain at least one digit");
                        break;
                    case AuthError.PasswordRequiresLower:
                        ModelState.AddModelError(nameof(RegistrationViewModel.Password),
                            "Password should contain at least one lowercase letter");
                        break;
                    case AuthError.PasswordRequiresUpper:
                        ModelState.AddModelError(nameof(RegistrationViewModel.Password),
                            "Password should contain at least one uppercase letter");
                        break;
                    case AuthError.PasswordRequiresUniqueChars:
                        ModelState.AddModelError(nameof(RegistrationViewModel.Password),
                            "Please add more unique symbols");
                        break;
                    case AuthError.PasswordRequiresNonAlphanumeric:
                        ModelState.AddModelError(nameof(RegistrationViewModel.Password),
                            "Password should contain at least one non alphanumeric symbol. Please add one of these: -._@+");
                        break;
                    case AuthError.PasswordConfirmationIncorrect:
                        ModelState.AddModelError(nameof(RegistrationViewModel.ConfirmPassword),
                            "The password and the confirmation password do not match.");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(error.ToString());
                }
            }
        }
    }
}
