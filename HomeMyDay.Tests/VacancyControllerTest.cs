using System;
using HomeMyDay.Controllers;
using HomeMyDay.Database;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeMyDay.Tests
{
    public class VacancyControllerTest
    {
        [Fact]
        public void TestEmptyVacancies()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
            IVacancieRepository repository = new EFVacancieRepository(context);

            VacancyController target = new VacancyController(repository);

            Assert.Empty(repository.Vacancies);
        }
    }
}
