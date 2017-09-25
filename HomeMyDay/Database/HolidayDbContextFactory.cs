using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HomeMyDay.Database
{
	public class HolidayDbContextFactory : IDesignTimeDbContextFactory<HolidayDbContext>
    {
	    public HolidayDbContext CreateDbContext(string[] args)
	    {
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

		    var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
		    optionsBuilder.UseSqlServer(config.GetConnectionString("HolidayConnection"));

		    return new HolidayDbContext(optionsBuilder.Options);
		}
    }
}
