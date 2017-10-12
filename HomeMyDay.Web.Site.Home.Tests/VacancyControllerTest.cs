using System;
using HomeMyDay.Core.Repository;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Repository;
using HomeMyDay.Web.Site.Home.Controllers;
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

            VacancyController target = new VacancyController(repository);

            Assert.Empty(repository.Vacancies);
        }
    }
}
