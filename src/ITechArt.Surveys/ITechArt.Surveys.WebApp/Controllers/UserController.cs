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
        public async Task<IActionResult> UserTable()
        {
            var users = await _userService.GetUsersShortData();
            var viewModel = new UserTableViewModel(users);

            return View(viewModel);
        }
    }
}
