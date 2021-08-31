using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITechArt.Surveys.Repositories
{
    public class DbInitializer
    {
        private const string AdminRoleName = "Admin";
        private const string UserRoleName = "User";
        private const string AdminDefaultEmail = "egorfedorenko.w@gmail.com";
        private const string AdminDefaultUserName = "EgorFedorenko";
        private const string AdminDefaultPassword = "paSSword1";


        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<SurveysDbContext>();

            string[] roleNames = { AdminRoleName, UserRoleName };

            foreach (var roleName in roleNames)
            {
                var roleStore = new RoleStore<Role>(context);

                if (!context.Set<Role>().Any(r => r.Name == roleName))
                {
                    await roleStore.CreateAsync(new Role { Name = roleName, NormalizedName = roleName.Normalize() });
                }
            }

            var user = new User
            {
                Email = AdminDefaultEmail,
                NormalizedEmail = AdminDefaultEmail.Normalize(),
                UserName = AdminDefaultUserName,
                NormalizedUserName = AdminDefaultUserName.Normalize()
            };

            if (!context.Set<User>().Any(u => u.UserRoles.Any(ur => ur.Role.Name == AdminRoleName)))
            {
                var passwordHasher = new PasswordHasher<User>();
                user.PasswordHash = passwordHasher.HashPassword(user, AdminDefaultPassword);

                var userStore = new UserStore<User>(context);
                await userStore.CreateAsync(user);
            }

            await AssignRoles(serviceProvider, user.Email, roleNames);

            await context.SaveChangesAsync();
        }


        private static async Task AssignRoles(IServiceProvider services, string email, IEnumerable<string> roles)
        {
            var userManager = services.GetService<UserManager<User>>();
            var user = await userManager.FindByEmailAsync(email);
            await userManager.AddToRolesAsync(user, roles);
        }
    }
}
