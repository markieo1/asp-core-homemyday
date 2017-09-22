using HomeMyDay.Controllers;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using HomeMyDay.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HomeMyDay.Tests
{
	public class SearchControllerTest
	{
		[Fact]
		public void TestEmptySearchAccommodations()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
			IHolidayRepository repository = new EFHolidayRepository(context);

			SearchController target = new SearchController(repository);

			HolidaySearchViewModel searchModel = new HolidaySearchViewModel()
			{
				StartDate = new DateTime(2017, 10, 12),
				EndDate = new DateTime(2017, 10, 22),
				Location = "Gilze",
				Persons = 4
			};

			HolidaySearchResultsViewModel results = target.Results(searchModel).Model as HolidaySearchResultsViewModel;

			Assert.NotNull(results);
			Assert.NotNull(results.Holidays);
			Assert.Empty(results.Holidays);
			Assert.NotNull(results.Search);
			Assert.Equal(searchModel, results.Search);
		}

		[Fact]
		public void TestFilledSearchAccommodations()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);

			context.Holidays.Add(new Holiday()
			{
				DepartureDate = new DateTime(2017, 10, 12),
				ReturnDate = new DateTime(2017, 10, 22),
				Accommodation = new Models.Accommodation()
				{
					Location = "Amsterdam",
					MaxPersons = 4
				}
			});

			context.SaveChanges();

			IHolidayRepository repository = new EFHolidayRepository(context);

			SearchController target = new SearchController(repository);

			HolidaySearchViewModel searchModel = new HolidaySearchViewModel()
			{
				StartDate = new DateTime(2017, 10, 12),
				EndDate = new DateTime(2017, 10, 22),
				Location = "Amsterdam",
				Persons = 4
			};

			HolidaySearchResultsViewModel results = target.Results(searchModel).Model as HolidaySearchResultsViewModel;

			Assert.NotNull(results);
			Assert.NotNull(results.Holidays);
			Assert.NotEmpty(results.Holidays);
			Assert.True(results.Holidays.Count() == 1);
			Assert.NotNull(results.Search);
			Assert.Equal(searchModel, results.Search);
		}
	}
}
