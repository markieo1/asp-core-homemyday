using HomeMyDay.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Database.Identity
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
		public static void Seed(AppIdentityDbContext context)
		{
			SeedRoles(context);
			SeedUser(context);
			context.SaveChangesAsync();
		}

		/// <summary>
		/// Seeds the roles.
		/// </summary>
		/// <param name="context">The context.</param>
		private static void SeedRoles(AppIdentityDbContext context)
		{
			RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);

			if (!context.Roles.Any(r => r.Name == IdentityRoles.Administrator))
			{
				roleStore.CreateAsync(new IdentityRole(IdentityRoles.Administrator));
			}

			if (!context.Roles.Any(r => r.Name == IdentityRoles.Booker))
			{
				roleStore.CreateAsync(new IdentityRole(IdentityRoles.Booker));
			}
		}

		/// <summary>
		/// Seeds the user.
		/// </summary>
		/// <param name="context">The context.</param>
		private static void SeedUser(AppIdentityDbContext context)
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

			if (!context.Users.Any(u => u.UserName == user.UserName))
			{
				var password = new PasswordHasher<User>();
				var hashed = password.HashPassword(user, "HomeMyDay@123");
				user.PasswordHash = hashed;
				var userStore = new UserStore<User>(context);
				userStore.CreateAsync(user);
				userStore.AddToRoleAsync(user, IdentityRoles.Administrator);
			}
		}
	}
}

