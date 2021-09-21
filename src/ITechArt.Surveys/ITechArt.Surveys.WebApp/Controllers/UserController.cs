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
        private const int UsersPerPage = 5;

        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserTable(int page = 1)
        {
            var skippedPages = page - 1;
            var users = await _userService.GetUsersRangeAsync(UsersPerPage, UsersPerPage * skippedPages);

            var usersForTable = users.Select(u => new UserDataForTable
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
                TotalUsersAmount = await _userService.CountUsersAsync()
            };

            return View(userTableViewModel);
        }
    }
}
