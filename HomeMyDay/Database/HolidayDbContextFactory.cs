using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;

namespace HomeMyDay.Database
{
	public class HolidayDbContextFactory : IDesignTimeDbContextFactory<HolidayDbContext>
	{
		public HolidayDbContext CreateDbContext(string[] args)
		{
			string basePath = AppContext.BaseDirectory;

			string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			IConfigurationBuilder builder = new ConfigurationBuilder()
				.SetBasePath(basePath)
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{envName}.json", true)
				.AddEnvironmentVariables();

			IConfiguration config = builder.Build();

			DbContextOptionsBuilder<HolidayDbContext> optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();

			optionsBuilder.UseSqlServer(config.GetConnectionString("HolidayConnection"));

			return new HolidayDbContext(optionsBuilder.Options);
		}
	}
}
