using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAuthorization_2_0.Data
{
    public class SampleData
    {
        public static async Task InitializeData(IServiceProvider services)
        {
            using (var serviceScope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var env = serviceScope.ServiceProvider.GetService<IHostingEnvironment>();
                if (!env.IsDevelopment()) return;

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var adminTask = roleManager.CreateAsync(new IdentityRole {Name = "Admin"});
                var powerUserTask = roleManager.CreateAsync(new IdentityRole {Name = "Power Users"});

                Task.WaitAll(adminTask, powerUserTask);

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                var user = new ApplicationUser
                {
                    Email = "luc@test.com",
                    UserName = "luc@test.com"
                };

                await userManager.CreateAsync(user, "Passw0rd!");

                //await userManager.AddToRoleAsync(user, "Admin");
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Country, "Belgium"));
            }
        }
    }
}