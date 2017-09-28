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
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			SearchController target = new SearchController(repository);

			AccommodationSearchViewModel searchModel = new AccommodationSearchViewModel()
			{
				StartDate = new DateTime(2017, 10, 12),
				EndDate = new DateTime(2017, 10, 22),
				Location = "Gilze",
				Persons = 4
			};

			ViewResult result = target.Results(searchModel);
			AccommodationSearchResultsViewModel model = result.Model as AccommodationSearchResultsViewModel;

			Assert.NotNull(model);
			Assert.NotNull(model.Search);
			Assert.Equal(0, model.Accommodations.Count());
			Assert.Equal(searchModel, model.Search);
			Assert.Equal("NoResults", result.ViewName);
		}

		[Fact]
		public void TestFilledSearchAccommodations()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);

			context.Accommodations.Add(new Accommodation()
			{
				NotAvailableDates = new List<DateEntity>()
				{
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 11)
					},
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 23)
					},
				},
				Location = "Amsterdam",
				MaxPersons = 4
			});

			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			SearchController target = new SearchController(repository);

			AccommodationSearchViewModel searchModel = new AccommodationSearchViewModel()
			{
				StartDate = new DateTime(2017, 10, 12),
				EndDate = new DateTime(2017, 10, 22),
				Location = "Amsterdam",
				Persons = 4
			};

			ViewResult result = target.Results(searchModel);
			AccommodationSearchResultsViewModel resultsModel = result.Model as AccommodationSearchResultsViewModel;

			Assert.NotNull(resultsModel);
			Assert.NotNull(resultsModel.Accommodations);
			Assert.NotEmpty(resultsModel.Accommodations);
			Assert.True(resultsModel.Accommodations.Count() == 1);
			Assert.NotNull(resultsModel.Search);
			Assert.Equal(searchModel, resultsModel.Search);
			Assert.Equal("Results", result.ViewName);
		}
	}
}
