using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ITechArt.Surveys.Repositories
{
    public static class DbSeedExtension
    {
        private const string AdminRoleName = "Admin";
        private const string UserRoleName = "User";
        private const string AdminDefaultEmail = "egorfedorenko.w@gmail.com";
        private const string AdminDefaultUserName = "EgorFedorenko";
        private const string AdminDefaultPassword = "paSSword1";


        public static async Task AddSeedDataToDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;
            var roleNames = await AddDefaultRoles(services);
            await AddAdmin(services, roleNames);
        }


        private static async Task<List<string>> AddDefaultRoles(IServiceProvider services)
        {
            var unitOfWork = services.GetService<IUnitOfWork>();
            var roleRepository = unitOfWork.GetRepository<Role>();
            var roleManager = services.GetService<RoleManager<Role>>();
            var roleNames = new List<string> { AdminRoleName, UserRoleName };

            foreach (var roleName in roleNames)
            {
                var isRoleAlreadyExist = await roleRepository.AnyAsync(r => r.Name == roleName);

                if (!isRoleAlreadyExist)
                {
                    await roleManager.CreateAsync(new Role { Name = roleName });
                }
            }

            return roleNames;
        }

        private static async Task AddAdmin(IServiceProvider services, List<string> roleNames)
        {
            var unitOfWork = services.GetService<IUnitOfWork>();
            var userRepository = unitOfWork.GetRepository<User>();
            var userManager = services.GetService<UserManager<User>>();
            var user = new User
            {
                Email = AdminDefaultEmail,
                UserName = AdminDefaultUserName
            };

            var isAdminAlreadyExist =
                await userRepository.AnyAsync(u => u.UserRoles.Any(ur => ur.Role.Name == AdminRoleName));

            if (!isAdminAlreadyExist)
            {
                await userManager.CreateAsync(user, AdminDefaultPassword);
                await userManager.AddToRolesAsync(user, roleNames);
            }
        }
    }
}
