using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace HomeMyDay.Database
{
	public class HomeMyDayDbContextFactory : IDesignTimeDbContextFactory<HomeMyDayDbContext>
	{
		public HomeMyDayDbContext CreateDbContext(string[] args)
		{
			string basePath = AppContext.BaseDirectory;

			string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			IConfigurationBuilder builder = new ConfigurationBuilder()
				.SetBasePath(basePath)
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{envName}.json", true)
				.AddEnvironmentVariables();

			IConfiguration config = builder.Build();

			DbContextOptionsBuilder<HomeMyDayDbContext> optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();

			optionsBuilder.UseSqlServer(config.GetConnectionString("HomeMyDayConnection"));

			return new HomeMyDayDbContext(optionsBuilder.Options);
		}
	}
}
