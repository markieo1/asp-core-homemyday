using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace HomeMyDay.Infrastructure.Database
{
	public class AppIdentityDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
	{
		public AppIdentityDbContext CreateDbContext(string[] args)
		{
			string basePath = AppContext.BaseDirectory;

			string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			IConfigurationBuilder builder = new ConfigurationBuilder()
				.SetBasePath(basePath)
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{envName}.json", true)
				.AddEnvironmentVariables();

			IConfiguration config = builder.Build();

			DbContextOptionsBuilder<AppIdentityDbContext> optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();
			optionsBuilder.UseSqlServer(config.GetConnectionString("IdentityConnection"));

			return new AppIdentityDbContext(optionsBuilder.Options);
		}
	}
}