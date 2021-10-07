using System;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.WebApp.Models;
using ITechArt.Surveys.WebApp.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITechArt.Surveys.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private const int UsersPerPage = 5;
        private const string SortUsersCookieName = "usersSortOrder";
        private const string AdminRoleName = "admin";

        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> UserTable(int page = 1, UserSortOrder? sortOrder = null)
        {
            if (sortOrder == null)
            {
                if (Request.Cookies.TryGetValue(SortUsersCookieName, out var sortOrderFromCookie))
                {
                    try
                    {
                        sortOrder = (UserSortOrder) Enum.Parse(typeof(UserSortOrder), sortOrderFromCookie);
                    }
                    catch
                    {
                        Response.Cookies.Append(SortUsersCookieName, UserSortOrder.DateDescending.ToString());
                        sortOrder = UserSortOrder.DateDescending;
                    }
                }
                else
                {
                    Response.Cookies.Append(SortUsersCookieName, UserSortOrder.DateDescending.ToString());
                    sortOrder = UserSortOrder.DateDescending;
                }
            }
            else
            {
                Response.Cookies.Append(SortUsersCookieName, sortOrder.ToString());
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortByClickOnName = sortOrder == UserSortOrder.Name
                ? UserSortOrder.NameDescending
                : UserSortOrder.Name;
            ViewBag.SortByClickOnRole = sortOrder == UserSortOrder.RoleDescending
                ? UserSortOrder.Role
                : UserSortOrder.RoleDescending;
            ViewBag.SortByClickOnDate = sortOrder == UserSortOrder.DateDescending
                ? UserSortOrder.Date
                : UserSortOrder.DateDescending;

            var skippedUsers = (page - 1) * UsersPerPage;
            var users = sortOrder switch
            {
                UserSortOrder.Name => await _userService
                    .GetPaginatedUsersAsync(skippedUsers, UsersPerPage, u => u.UserName),
                UserSortOrder.NameDescending => await _userService
                    .GetPaginatedUsersAsync(skippedUsers, UsersPerPage, u => u.UserName, true),
                UserSortOrder.Role => await _userService
                    .GetPaginatedUsersAsync(skippedUsers, UsersPerPage, u => u.UserRoles.Count),
                UserSortOrder.RoleDescending => await _userService
                    .GetPaginatedUsersAsync(skippedUsers, UsersPerPage, u => u.UserRoles.Count, true),
                UserSortOrder.Date => await _userService
                    .GetPaginatedUsersAsync(skippedUsers, UsersPerPage, u => u.RegistrationDate),
                UserSortOrder.DateDescending => await _userService
                    .GetPaginatedUsersAsync(skippedUsers, UsersPerPage, u => u.RegistrationDate, true),
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
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _userService.DeleteUserAsync(userId);

            return RedirectToAction("UserTable");
        }

        [HttpPost]
        public async Task<IActionResult> GiveAdminRights(string userId)
        {
            await _userService.AddToRoleAsync(userId, AdminRoleName);

            return RedirectToAction("UserTable");
        }

        [HttpPost]
        public async Task<IActionResult> RevokeAdminRights(string userId)
        {
            await _userService.RemoveFromRoleAsync(userId, AdminRoleName);

            return RedirectToAction("UserTable");
        }
    }
}
