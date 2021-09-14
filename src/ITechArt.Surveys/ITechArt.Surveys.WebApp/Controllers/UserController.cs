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


        public async Task<IActionResult> UserTable()
        {
            var users = await _userService.GetUsersShortData();
            var viewModel = new UserTableViewModel();
            foreach (var user in users)
            {
                var userViewModel = new UserInTableViewModel
                {
                    Name = user.Name,
                    RegistrationDate = user.RegistrationDate,
                    Role = user.Role
                };
                viewModel.Users.Add(userViewModel);
            }

            viewModel.UsersCounter = viewModel.Users.Capacity;

            return View(viewModel);
        }
    }
}
