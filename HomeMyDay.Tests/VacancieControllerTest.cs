using System;
using System.Collections.Generic;
using System.Text;
using HomeMyDay.Controllers;
using HomeMyDay.Database;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeMyDay.Tests
{
    public class VacancieControllerTest
    {
	    [Fact]
	    public void TestEmptyVacancies()
	    {
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
		    optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
		    HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IVacancieRepository repository = new EFVacancieRepository(context);

			VacancieController target = new VacancieController(repository);

			VacancieView
		}
	}
}
