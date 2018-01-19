﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Repository;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Web.Base.Managers.Implementation;
using HomeMyDay.Web.Base.ViewModels;
using HomeMyDay.Web.Site.Home.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;

namespace HomeMyDay.Web.Site.Home.Tests
{
	[TestClass]
	[Ignore]
	public class ReviewControllerTest
	{
		[TestMethod][Ignore]
		public void TestEmptyReviews()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);
			IReviewRepository reviewRepository = new EFReviewRepository(context, null);
			IReviewManager reviewManager = new ReviewManager(reviewRepository);	  

			var target = new ReviewController(reviewManager);

			var result = target.Index();
			var model = result.Model as IEnumerable<ReviewViewModel>;

			Xunit.Assert.NotNull(model);
			Xunit.Assert.True(!model.Any());
		}

		[TestMethod][Ignore]
		public void TestFilledReviews()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Reviews.Add(new Review()
			{
				Id = 1,
				Name = "TestReview",
				Accommodation = new Accommodation() { Id = "1", Name = "TestAccommodation" },
				Approved = true,
				Date = DateTime.Now,
				Title = "Test",
				Text = "Dit was een goede vakantie!"
			});

			context.SaveChanges();

			IReviewRepository reviewRepository = new EFReviewRepository(context, null);
			IReviewManager reviewManager = new ReviewManager(reviewRepository);

			var target = new ReviewController(reviewManager);

			var result = target.Index();
			var model = result.Model as IEnumerable<ReviewViewModel>;

			Xunit.Assert.NotNull(model);
			Xunit.Assert.Equal("TestReview", model.FirstOrDefault().Name);
		}

		[TestMethod][Ignore]
		public void TestAddReview()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);

			var accommodation = new Accommodation()
			{
				Id = "1",
				Name = "TestAccommodation"
			};

			context.Accommodations.Add(accommodation);
			context.SaveChanges();

			IAccommodationRepository accommodationRepository = new EFAccommodationRepository(context);

			IReviewRepository reviewRepository = new EFReviewRepository(context, accommodationRepository);

			var dummy = Encoding.ASCII.GetBytes("{}");
			var sessionMock = new Mock<ISession>();
			sessionMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out dummy)).Returns(true).Verifiable();

			var httpContext = new DefaultHttpContext
			{
				Session = sessionMock.Object
			};

			IReviewManager reviewManager = new ReviewManager(reviewRepository);

			var target = new ReviewController(reviewManager)
			{
				TempData = new TempDataDictionary(httpContext, new SessionStateTempDataProvider())
			};

			ReviewViewModel reviewViewModelToAdd = new ReviewViewModel()
			{
				AccommodationId = accommodation.Id,
				Name = "TestReviewAddTest",
				Approved = true,
				Date = DateTime.Now,
				Title = "TestReviewAdd",
				Text = "Dit was goed!"
			};

			var result = target.AddReview(reviewViewModelToAdd) as RedirectToActionResult;
			Xunit.Assert.NotNull(result.ActionName);
			Xunit.Assert.NotNull(result.ControllerName);
			Xunit.Assert.Equal("Detail", result.ActionName);
			Xunit.Assert.Equal("Accommodation", result.ControllerName);
		}
	}
}
