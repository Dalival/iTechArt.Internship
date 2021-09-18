using System.Linq;
using System.Threading.Tasks;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserTable(int page = 1)
        {
            var allUsers = await _userService.GetAllUsersIncludeRoles();

            var usersForTable = allUsers
                .Skip(5 * (page - 1))
                .Take(5)
                .Select(u => new UserDataForTable
                {
                    Name = u.UserName,
                    RegistrationDate = u.RegistrationDate,
                    Role = u.UserRoles.Any(ur => ur.Role.Name != "User")
                        ? string.Join(", ", u.UserRoles.Select(ur => ur.Role.Name).Where(n => n != "User"))
                        : "User"
                })
                .ToList();

            var userTableViewModel = new UserTableViewModel
            {
                Users = usersForTable,
                Page = page,
                TotalUsersAmount = allUsers.Count
            };

            return View(userTableViewModel);
        }
    }
}
