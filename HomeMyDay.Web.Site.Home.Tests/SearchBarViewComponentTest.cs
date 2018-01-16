using System;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Repository;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Web.Base.Managers.Implementation;
using HomeMyDay.Web.Base.ViewModels;
using HomeMyDay.Web.Site.Home.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace HomeMyDay.Web.Site.Home.Tests
{
	[TestClass]
	[Ignore]
	public class SearchBarViewComponentTest
	{
		[TestMethod][Ignore]
		public void TestEmptySearchAccommodations()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);
			IReviewRepository reviewRepo = new EFReviewRepository(context, repository);
			IAccommodationManager manager = new AccommodationManager(repository, reviewRepo);

			SearchBarViewComponent target = new SearchBarViewComponent(manager);

			AccommodationSearchViewModel results = (AccommodationSearchViewModel)(target.Invoke() as ViewViewComponentResult).ViewData.Model;

			Xunit.Assert.NotNull(results);
			Xunit.Assert.Empty(results.Accommodations);
		}

		[TestMethod][Ignore]
		public void TestFilledSearchAccommodations()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.Add(new Accommodation()
			{
				Beds = 4,
				Continent = "Europe",
				Country = "Netherlands",
				Description = "Van der Valk hotels",
				Location = "Gilze",
				MaxPersons = 4,
				Name = "Van der valk Gilze",
				Rooms = 5
			});

			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);
			IReviewRepository reviewRepo = new EFReviewRepository(context, repository);
			IAccommodationManager manager = new AccommodationManager(repository, reviewRepo);

			SearchBarViewComponent target = new SearchBarViewComponent(manager);

			AccommodationSearchViewModel results = (AccommodationSearchViewModel)(target.Invoke() as ViewViewComponentResult).ViewData.Model;

			Xunit.Assert.NotNull(results);
			Xunit.Assert.NotEmpty(results.Accommodations);
		}
	}
}
