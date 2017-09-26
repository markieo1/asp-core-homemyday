using HomeMyDay.Controllers;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

			ViewResult result = target.Results(searchModel);
			HolidaySearchResultsViewModel model = result.Model as HolidaySearchResultsViewModel;

			Assert.NotNull(model);
			Assert.NotNull(model.Search);
			Assert.Equal(0, model.Holidays.Count());
			Assert.Equal(searchModel, model.Search);
			Assert.Equal("NoResults", result.ViewName);
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

			ViewResult result = target.Results(searchModel);
			HolidaySearchResultsViewModel resultsModel = result.Model as HolidaySearchResultsViewModel;

			Assert.NotNull(resultsModel);
			Assert.NotNull(resultsModel.Holidays);
			Assert.NotEmpty(resultsModel.Holidays);
			Assert.True(resultsModel.Holidays.Count() == 1);
			Assert.NotNull(resultsModel.Search);
			Assert.Equal(searchModel, resultsModel.Search);
			Assert.Equal("Results", result.ViewName);
		}
	}
}
