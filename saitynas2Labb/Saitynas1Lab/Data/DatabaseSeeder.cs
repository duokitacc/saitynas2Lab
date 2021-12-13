using Microsoft.AspNetCore.Identity;
using Saitynas1Lab.Auth.Model;
using Saitynas1Lab.Data.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<DemoRestUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseSeeder(UserManager<DemoRestUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedAsync()
        {
            foreach(var role in DemoRestUserRoles.All)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
                var newAdminUser = new DemoRestUser
                {
                    UserName = DemoRestUserRoles.Admin,
                    Email = "admin@admin.com"
                };
                //var newSimpleUser = new DemoRestUser
                //{
                //    UserName = "paprastasis",
                //    Email = "praprastasis@gmail.com"
                //};
                var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
                if(existingAdminUser == null)
                {
                    var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword1!");
                    if (createAdminUserResult.Succeeded)
                    {
                        await _userManager.AddToRolesAsync(newAdminUser, DemoRestUserRoles.All);
                    }

                }
                //var existingSimpleUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
                //if (existingSimpleUser == null)
                //{
                //    var createSimpleUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword2!");
                //    if (createSimpleUserResult.Succeeded)
                //    {
                //        await _userManager.AddToRoleAsync(newSimpleUser, DemoRestUserRoles.SimpleUser);
                //    }

                //}
            }
        }
    }
}
