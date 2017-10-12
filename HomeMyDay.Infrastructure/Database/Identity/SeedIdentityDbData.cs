using HomeMyDay.Core.Models;
using HomeMyDay.Web.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Infrastructure.Database
{
	public static class SeedIdentityDbData
	{
		/// <summary>
		/// The default admin username
		/// </summary>
		private const string AdminUsername = "Administrator";

		/// <summary>
		/// Seeds the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="services">The app services.</param>
		public static void Seed(AppIdentityDbContext context, IServiceProvider services)
		{
			var task = Task.Run(async () =>
			{
				await SeedRoles(context);

				using (var serviceScope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
				{
					UserManager<User> userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
					await SeedUser(userManager);
				}

				await context.SaveChangesAsync();
			});

			task.Wait();
		}

		/// <summary>
		/// Seeds the roles.
		/// </summary>
		/// <param name="context">The context.</param>
		private static async Task SeedRoles(AppIdentityDbContext context)
		{
			RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);

			if (!context.Roles.Any(r => r.NormalizedName == IdentityRoles.Administrator))
			{
				await roleStore.CreateAsync(new IdentityRole()
				{
					Name = IdentityRoles.Administrator,
					NormalizedName = IdentityRoles.Administrator
				});
			}

			if (!context.Roles.Any(r => r.NormalizedName == IdentityRoles.Booker))
			{
				await roleStore.CreateAsync(new IdentityRole()
				{
					Name = IdentityRoles.Booker,
					NormalizedName = IdentityRoles.Booker
				});
			}

			await context.SaveChangesAsync();
		}

		/// <summary>
		/// Seeds the user.
		/// </summary>
		/// <param name="userManager">The user manager</param>
		private static async Task SeedUser(UserManager<User> userManager)
		{
			User user = new User
			{
				UserName = AdminUsername,
				NormalizedUserName = AdminUsername,
				Email = "info@homemyday.nl",
				NormalizedEmail = "info@homemyday.nl",
				EmailConfirmed = true,
				LockoutEnabled = false,
				SecurityStamp = Guid.NewGuid().ToString()
			};

			if (!userManager.Users.Any(u => u.UserName == user.UserName))
			{
				var password = new PasswordHasher<User>();
				var hashed = password.HashPassword(user, "HomeMyDay@123");
				user.PasswordHash = hashed;

				await userManager.CreateAsync(user);
			}

			User adminUser = await userManager.FindByNameAsync(AdminUsername);

			await userManager.AddToRoleAsync(adminUser, IdentityRoles.Administrator);
		}
	}
}

