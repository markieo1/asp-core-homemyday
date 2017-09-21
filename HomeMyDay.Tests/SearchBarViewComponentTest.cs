using HomeMyDay.Components;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HomeMyDay.Tests
{
	public class SearchBarViewComponentTest
	{
		[Fact]
		public void TestEmptySearchArrangements()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
			IHolidayRepository repository = new EFHolidayRepository(context);

			SearchBarViewComponent target = new SearchBarViewComponent(repository);

			HolidaySearchViewModel results = (HolidaySearchViewModel)(target.Invoke() as ViewViewComponentResult).ViewData.Model;

			Assert.NotNull(results);
			Assert.Empty(results.Accommodations);
		}

		[Fact]
		public void TestFilledSearchArrangements()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);

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

			IHolidayRepository repository = new EFHolidayRepository(context);

			SearchBarViewComponent target = new SearchBarViewComponent(repository);

			HolidaySearchViewModel results = (HolidaySearchViewModel)(target.Invoke() as ViewViewComponentResult).ViewData.Model;

			Assert.NotNull(results);
			Assert.NotEmpty(results.Accommodations);
		}
	}
}
