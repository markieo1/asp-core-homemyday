﻿using System;
using System.Collections.Generic;
using System.Linq;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Repository;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Web.Base.Managers.Implementation;
using HomeMyDay.Web.Base.ViewModels;
using HomeMyDay.Web.Site.Home.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace HomeMyDay.Web.Site.Home.Tests
{
	[TestClass]
	[Ignore]
	public class SearchControllerTest
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

			SearchController target = new SearchController(manager);

			AccommodationSearchViewModel searchModel = new AccommodationSearchViewModel()
			{
				StartDate = new DateTime(2017, 10, 12),
				EndDate = new DateTime(2017, 10, 22),
				Location = "Gilze",
				Persons = 4
			};

			ViewResult result = target.Results(searchModel);
			AccommodationSearchResultsViewModel model = result.Model as AccommodationSearchResultsViewModel;

			Xunit.Assert.NotNull(model);
			Xunit.Assert.NotNull(model.Search);
			Xunit.Assert.Empty(model.Accommodations);
			Xunit.Assert.Equal(searchModel, model.Search);
			Xunit.Assert.Equal("NoResults", result.ViewName);
		}

		[TestMethod][Ignore]
		public void TestFilledSearchAccommodations()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

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
			IReviewRepository reviewRepo = new EFReviewRepository(context, repository);
			IAccommodationManager manager = new AccommodationManager(repository, reviewRepo);

			SearchController target = new SearchController(manager);

			AccommodationSearchViewModel searchModel = new AccommodationSearchViewModel()
			{
				StartDate = new DateTime(2017, 10, 12),
				EndDate = new DateTime(2017, 10, 22),
				Location = "Amsterdam",
				Persons = 4
			};

			ViewResult result = target.Results(searchModel);
			AccommodationSearchResultsViewModel resultsModel = result.Model as AccommodationSearchResultsViewModel;

			Xunit.Assert.NotNull(resultsModel);
			Xunit.Assert.NotNull(resultsModel.Accommodations);
			Xunit.Assert.NotEmpty(resultsModel.Accommodations);
			Xunit.Assert.True(resultsModel.Accommodations.Count() == 1);
			Xunit.Assert.NotNull(resultsModel.Search);
			Xunit.Assert.Equal(searchModel, resultsModel.Search);
			Xunit.Assert.Equal("Results", result.ViewName);
		}
	}
}
