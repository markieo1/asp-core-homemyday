using System;
using System.Linq;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using Xunit;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HomeMyDay.Tests
{
	public class EFVacancieRepositoryTest
    {
	    [Fact]
	    public void TestGetIdIsZeroVacancie()
	    {
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
		    optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
		    HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IVacancyRepository repository = new EFVacancyRepository(context);

		    Assert.Throws<ArgumentOutOfRangeException>(() => repository.GetVacancy(0));
	    }

	    [Fact]
	    public void TestGetIdBelowZeroVacancie()
	    {
		    var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
		    optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
		    HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
		    IVacancyRepository repository = new EFVacancyRepository(context);

		    Assert.Throws<ArgumentOutOfRangeException>(() => repository.GetVacancy(-1));
	    }	

		[Fact]
	    public void TestGetIdNotExistingVacancie()
	    {
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
		    optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
		    HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

		    context.Vacancies.Add(new Vacancy()
		    {
				Id = 1,
				JobTitle = "Test",
				AboutFunction = "Software Engineer",
				AboutVacancy = "Fulltime",
				City = "Breda",
				Company = "SPIE Nederland B.V.",
				JobRequirements = "HBO Bachelor",
				WeOffer = "Luxe secundaire voorwaarden"
		    });

		    context.SaveChanges();

			IVacancyRepository repository = new EFVacancyRepository(context);

		    Assert.Throws<KeyNotFoundException>(() => repository.GetVacancy(2));
	    }

	    public void TestGetIdExistingAccommodation()
	    {
		    var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
		    optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
		    HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

		    context.Vacancies.Add(new Vacancy()
		    {
				Id = 1,
			    JobTitle = "Test",
			    AboutFunction = "Software Engineer",
			    AboutVacancy = "Fulltime",
			    City = "Breda",
			    Company = "SPIE Nederland B.V.",
			    JobRequirements = "HBO Bachelor",
			    WeOffer = "Luxe secundaire voorwaarden"

			});

		    context.SaveChanges();

		    IVacancyRepository repository = new EFVacancyRepository(context);

		    var vacancie = repository.GetVacancy(1);

		    Assert.NotNull(vacancie);			   
	    }

		[Fact]
		public void TestVacanciesEmptyRepository()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IVacancyRepository repository = new EFVacancyRepository(context);

			Assert.Empty(repository.Vacancies);
		}

	    [Fact]
	    public void TestVacanciesFilledRepository()
	    {
		    var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
		    optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
		    HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

		    context.Vacancies.AddRange(
			    new Vacancy() { JobTitle = "Dit is een omschrijving", AboutFunction = "Test0"},
			    new Vacancy() { JobTitle = "Dit is een omschrijving", AboutFunction = "Test1"},
			    new Vacancy() { JobTitle = "Dit is een omschrijving", AboutFunction = "Test2"},
			    new Vacancy() { JobTitle = "Dit is een omschrijving", AboutFunction = "Test3" }
		    );
		    context.SaveChanges();

		    IVacancyRepository repository = new EFVacancyRepository(context);

		    Assert.True(repository.Vacancies.Count() == 4);
	    }  
	}
}
