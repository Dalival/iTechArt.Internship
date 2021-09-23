using System.Linq;
using System.Threading.Tasks;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITechArt.Surveys.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private const int UsersPerPage = 5;

        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> UserTable(int page = 1)
        {
            var skippedPages = page - 1;
            var users = await _userService.GetPaginatedUsersAsync(UsersPerPage * skippedPages, UsersPerPage);

            var usersForTable = users.Select(u => new UserDataForTable
                {
                    Id = u.Id,
                    Name = u.UserName,
                    RegistrationDate = u.RegistrationDate,
                    Role = string.Join(", ", u.UserRoles.Select(ur => ur.Role.Name))
                })
                .ToList();

            var totalUsersAmount = await _userService.CountUsersAsync();

            var allRoles = await _userService.GetAllRolesAsync();
            var rolesNames = allRoles.Select(r => r.Name);

            var userTableViewModel = new UserTableViewModel
            {
                Users = usersForTable,
                Page = page,
                TotalUsersAmount = totalUsersAmount,
                Roles = rolesNames
            };

            return View(userTableViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserAsync(id);

            return RedirectToAction("UserTable");
        }

        [HttpGet]
        public async Task<IActionResult> AddToRole(string userId, string roleName)
        {
            await _userService.AddToRoleAsync(userId, roleName);

            return RedirectToAction("UserTable");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromRole(string userId, string roleName)
        {
            await _userService.RemoveFromRoleAsync(userId, roleName);

            return RedirectToAction("UserTable");
        }
    }
}
