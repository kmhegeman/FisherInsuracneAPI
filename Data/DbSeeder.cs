using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FisherInsuranceApi.Data
{
    public class DbSeeder
    {

        private FisherContext db;
        private RoleManager<IdentityRole> RoleManager;
        private UserManager<ApplicationUser> UserManager;

        public DbSeeder(FisherContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            db = dbContext;
            RoleManager = roleManager;
            UserManager = userManager;
        }

        public async Task SeedAsync()
        {
            // Create the Db if it doesn’t exist
            db.Database.EnsureCreated();

            // Create default Users
            await CreateUsersAsync();

        }

        private async Task CreateUsersAsync()
        {
            // local variables
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;
            string role_Administrators = "Administrators";
            string role_Registered = "Registered";

            //Create Roles (if they doesn't exist yet)
            if (!await RoleManager.RoleExistsAsync(role_Administrators))
            {
                await RoleManager.CreateAsync(new IdentityRole(role_Administrators));
            }
            if (!await RoleManager.RoleExistsAsync(role_Registered))
            {
                await RoleManager.CreateAsync(new IdentityRole(role_Registered));
            }

            // Create the "Admin" ApplicationUser account (if it doesn't exist already)
            var user_Admin = new ApplicationUser()
            {
                UserName = "Admin",
                Email = "admin@fisherinsurance.com"
            };

            await CreateUserAsync(user_Admin, role_Administrators);

            // Create some sample registered user accounts (if they don't exist already)
            var user_Jeff = new ApplicationUser()
            {
                UserName = "Jeff",
                Email = "Jeff@fisherinsurance.com"
            };

            await CreateUserAsync(user_Jeff, role_Registered);

            var user_Nancy = new ApplicationUser()
            {
                UserName = "Nancy",
                Email = "Nancy@fisherinsurance.com"
            };

            await CreateUserAsync(user_Nancy, role_Registered);

            user_Admin.EmailConfirmed = true;
            user_Admin.LockoutEnabled = false;

            user_Jeff.EmailConfirmed = true;
            user_Jeff.LockoutEnabled = false;

            user_Nancy.EmailConfirmed = true;
            user_Nancy.LockoutEnabled = false;

            await db.SaveChangesAsync();
        }

        private async Task CreateUserAsync(ApplicationUser user, string role)
        {
            if (await UserManager.FindByEmailAsync(user.Email) == null)
            {
                await UserManager.CreateAsync(user, "P@ssw0rd");
                await UserManager.AddToRoleAsync(user, role);
            }
        }
    }
}