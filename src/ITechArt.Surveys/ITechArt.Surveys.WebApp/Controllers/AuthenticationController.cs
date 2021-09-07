using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.Foundation.Result;
using ITechArt.Surveys.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITechArt.Surveys.WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;


        public AuthenticationController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
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

            var result = await _userService.CreateUserAsync(user, model.Password);
            if (!result.Succeeded)
            {
                AddErrorsToModel(result.Errors);

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _accountService.LoginAsync(model.EmailOrUserName, model.Password);
            if (!result.Succeeded)
            {
                AddErrorsToModel(result.Errors);

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();

            return RedirectToAction("Index", "Home");
        }


        private void AddErrorsToModel(IEnumerable<RegistrationError> errors)
        {
            foreach (var error in errors)
            {
                var (key, message) = error switch
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

                ModelState.AddModelError(key, message);
            }
        }

        private void AddErrorsToModel(IEnumerable<LoginError> errors)
        {
            foreach (var error in errors)
            {
                var (key, message) = error switch
                {
                    LoginError.AccountLockedOut => (nameof(LoginViewModel.EmailOrUserName),
                        "Account is locked out now. Try again later."),
                    LoginError.NotAllowedToLogin => (nameof(LoginViewModel.EmailOrUserName),
                        "Sorry, you are not allowed to login. Try again later."),
                    LoginError.EmailAndUserNameNotFound => (nameof(LoginViewModel.EmailOrUserName),
                        "User is not found"),
                    LoginError.WrongPassword => (nameof(LoginViewModel.Password),
                        "Wrong password"),
                    _ => throw new ArgumentOutOfRangeException(error.ToString())
                };

                ModelState.AddModelError(key, message);
            }
        }
    }
}
