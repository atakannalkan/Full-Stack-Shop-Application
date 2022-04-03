using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace shopapp.webui.Identity
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager,IConfiguration configuration)
        {
            // Role Create
            var roles = configuration.GetSection("SeedIdentity:Roles").GetChildren().Select(i => i.Value).ToArray();

            foreach (var roleName in roles)
            {
                if (await _roleManager.RoleExistsAsync(roleName) == false)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // User Create
            var users = configuration.GetSection("SeedIdentity:Users").GetChildren();

            foreach (var section in users)
            {
                var firstName = section.GetValue<string>("firstName");
                var lastName = section.GetValue<string>("lastName");
                var userName = section.GetValue<string>("userName");
                var email = section.GetValue<string>("email");
                var emailConfirmed = section.GetValue<bool>("emailConfirmed");
                var role = section.GetValue<string>("role");
                var password = section.GetValue<string>("password");

                if (await _userManager.FindByNameAsync(userName) == null)
                {
                    var user = new User()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        UserName = userName,
                        Email = email,
                        EmailConfirmed = emailConfirmed,
                        DateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                        CopyPassword = password
                    };

                    var result = await _userManager.CreateAsync(user,password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user,role);
                    }
                }
            }            
        }
    }
}