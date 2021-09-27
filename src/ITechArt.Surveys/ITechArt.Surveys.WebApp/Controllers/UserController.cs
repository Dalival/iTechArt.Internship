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

            var usersForTable = users.Select(u => new UserDataForTableViewModel
                {
                    Id = u.Id,
                    Name = u.UserName,
                    RegistrationDate = u.RegistrationDate,
                    Roles = u.UserRoles.Select(ur => new RoleViewModel
                    {
                        Id = ur.RoleId,
                        Name = ur.Role.Name
                    })
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
        public async Task<IActionResult> GiveAdminRights(string userId)
        {
            await _userService.RemoveFromRoleAsync(userId, "User");
            await _userService.AddToRoleAsync(userId, "Admin");

            return RedirectToAction("UserTable");
        }

        [HttpGet]
        public async Task<IActionResult> RevokeAdminRights(string userId)
        {
            await _userService.RemoveFromRoleAsync(userId, "Admin");
            await _userService.AddToRoleAsync(userId, "User");

            return RedirectToAction("UserTable");
        }
    }
}
