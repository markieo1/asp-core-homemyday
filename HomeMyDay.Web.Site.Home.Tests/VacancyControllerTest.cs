using System;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Repository;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Web.Base.Managers.Implementation;
using HomeMyDay.Web.Site.Home.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeMyDay.Web.Site.Home.Tests
{
	public class VacancyControllerTest
    {
        [Fact]
        public void TestEmptyVacancies()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
            IVacancyRepository repository = new EFVacancyRepository(context);
			IVacancyManager manager = new VacancyManager(repository);

            VacancyController target = new VacancyController(manager);

	        var result = target.Index() as ViewResult;
	        var model = result.Model as Vacancy;

            Assert.Null(model);
        }
    }
}
