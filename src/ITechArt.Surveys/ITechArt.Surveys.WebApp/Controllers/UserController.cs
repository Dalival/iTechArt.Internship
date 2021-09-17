using System.Linq;
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
            var users = await _userService.GetAllUsersIncludeRoles();
            var usersForTable = users.Select(u => new UserInTableViewModel
                {
                    Name = u.UserName,
                    RegistrationDate = u.RegistrationDate,
                    Role = u.UserRoles.Any(ur => ur.Role.Name != "User")
                        ? string.Join(", ", u.UserRoles.Select(ur => ur.Role.Name).Where(n => n != "User"))
                        : "User"
                })
                .ToList();

            return View(usersForTable);
        }
    }
}
