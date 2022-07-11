using System;
using System.Threading.Tasks;
using Data.AppContext;
using Entities.AppIdentities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using WebFramework.Extensions;

namespace Pula_Store.WebFramework.Extensions
{
    public static class WebHostExtensions
    {
        public static async Task<IWebHost> SeedDataAsync(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<AppDbContext>();
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

                #region commented Codes
                //var passwordHasher= serviceProvider.GetRequiredService<IPasswordHasher<AppUser>>();

                //if (context.Database.DbExist())
                //{
                //    var anyUser = await context.Users.AnyAsync();
                //    if (!anyUser)
                //        await AddUsersWithClaim(context, userManager);
                //}
                //else
                //{
                //    context.Database.Migrate();
                //    await AddUsersWithClaim(context, userManager);
                //}
                #endregion 

                if (!context.Database.DbExist())
                    context.Database.Migrate();

                var anyUser = await context.Users.AnyAsync();
                if (!anyUser)
                    await AddUsersWithClaim(context, userManager);
            }

            return host;
        }

        private static async Task AddUsersWithClaim(
            AppDbContext context,
            UserManager<AppUser> userManager)
        {
            var userAdmin = new AppUser()
            {
                UserName = "saman",
                Email = "saman@pula.com",
                PhoneNumber = "09911410142",
                IsActive = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var userOperator = new AppUser()
            {
                UserName = "operator",
                Email = "saman@pula.com",
                PhoneNumber = "09911410142",
                IsActive = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var user = new AppUser()
            {
                UserName = "Appuser",
                Email = "saman@pula.com",
                IsActive = true,
                PhoneNumber = "09911410142",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            context.Users.Add(userAdmin);
            context.Users.Add(userOperator);
            context.Users.Add(user);

            await context.SaveChangesAsync();

            ///adding Claims to user should be done after creating and adding user to db
            ///with that method ;)
            await userManager.AddClaimAsync(userAdmin, new Claim("Role", "Admin"));
            ///adding password to user, should be done after creating and adding user to db
            ///with that method ;)
            await userManager.AddPasswordAsync(userAdmin, "lordhacker");

            await userManager.AddClaimAsync(userOperator, new Claim("Role", "Operator"));
            await userManager.AddPasswordAsync(userOperator, "lordhacker");

            await userManager.AddClaimAsync(user, new Claim("Role", "User"));
            await userManager.AddPasswordAsync(user, "lordhacker");

            await context.SaveChangesAsync();
        }
    }
}
