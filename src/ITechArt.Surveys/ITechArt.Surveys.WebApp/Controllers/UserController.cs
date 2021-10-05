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
        public async Task<IActionResult> UserTable(int page = 1, string sortOrder = "date_desc")
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortByClickOnName = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.SortByClickOnRole = sortOrder == "role_desc" ? "role" : "role_desc";
            ViewBag.SortByClickOnDate = sortOrder == "date_desc" ? "date" : "date_desc";

            var skippedUsers = (page - 1) * UsersPerPage;
            var users = sortOrder switch
            {
                "name" => await _userService.GetPaginatedUsersAsync(skippedUsers, UsersPerPage, u => u.UserName),
                "name_desc" => await _userService.GetPaginatedUsersAsync(skippedUsers, UsersPerPage, u => u.UserName, true),
                "role" => await _userService.GetPaginatedUsersAsync(skippedUsers, UsersPerPage, u => u.UserRoles.Count),
                "role_desc" => await _userService.GetPaginatedUsersAsync(skippedUsers, UsersPerPage, u => u.UserRoles.Count, true),
                "date" => await _userService.GetPaginatedUsersAsync(skippedUsers, UsersPerPage, u => u.RegistrationDate),
                "date_desc" => await _userService.GetPaginatedUsersAsync(skippedUsers, UsersPerPage, u => u.RegistrationDate, true),
                _ => await _userService.GetPaginatedUsersAsync(skippedUsers, UsersPerPage)
            };

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
            var userTableViewModel = new UserTableViewModel
            {
                Users = usersForTable,
                Page = page,
                TotalUsersAmount = totalUsersAmount
            };

            return View(userTableViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId, string sortOrder)
        {
            await _userService.DeleteUserAsync(userId);

            return RedirectToAction("UserTable", new { sortOrder });
        }

        [HttpPost]
        public async Task<IActionResult> GiveAdminRights(string userId, string sortOrder)
        {
            await _userService.AddToRoleAsync(userId, "admin");

            return RedirectToAction("UserTable", new { sortOrder });
        }

        [HttpPost]
        public async Task<IActionResult> RevokeAdminRights(string userId, string sortOrder)
        {
            await _userService.RemoveFromRoleAsync(userId, "admin");

            return RedirectToAction("UserTable", new { sortOrder });
        }
    }
}
