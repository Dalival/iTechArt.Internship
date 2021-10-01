using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.WebApp.Models;
using ITechArt.Surveys.WebApp.Models.Enums;
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
        public async Task<IActionResult> UserTable(int page = 1, UserSortingKey sortBy = UserSortingKey.RegistrationDate)
        {
            var skippedPages = page - 1;
            var users = sortBy switch
            {
                UserSortingKey.Name => await _userService.GetPaginatedUsersAsync(
                    UsersPerPage * skippedPages, UsersPerPage, u => u.UserName),
                UserSortingKey.Role => await _userService.GetPaginatedUsersAsync(
                    UsersPerPage * skippedPages, UsersPerPage, u => u.UserRoles.Count),
                _ => await _userService.GetPaginatedUsersAsync(
                    UsersPerPage * skippedPages, UsersPerPage, u => u.RegistrationDate)
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
            await _userService.AddToRoleAsync(userId, "admin");

            return RedirectToAction("UserTable");
        }

        [HttpPost]
        public async Task<IActionResult> RevokeAdminRights(string userId)
        {
            await _userService.RemoveFromRoleAsync(userId, "admin");

            return RedirectToAction("UserTable");
        }
    }
}
