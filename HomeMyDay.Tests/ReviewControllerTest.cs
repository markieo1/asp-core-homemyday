using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeMyDay.Core.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Repository;
using HomeMyDay.Core.Repository;
using HomeMyDay.Web.Site.Home.Controllers;
using HomeMyDay.Web.Base.Home.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace HomeMyDay.Tests
{
	public class ReviewControllerTest
	{
		[Fact]
		public void TestEmptyReviews()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);
			IReviewRepository reviewRepository = new EFReviewRepository(context, null);

			var target = new ReviewController(reviewRepository);

			var result = target.Index();
			var model = result.Model as IEnumerable<ReviewViewModel>;

			Assert.NotNull(model);
			Assert.True(!model.Any());
		}

		[Fact]
		public void TestFilledReviews()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Reviews.Add(new Review()
			{
				Id = 1,
				Name = "TestReview",
				Accommodation = new Accommodation() { Id = 1, Name = "TestAccommodation" },
				Approved = true,
				Date = DateTime.Now,
				Title = "Test",
				Text = "Dit was een goede vakantie!"
			});

			context.SaveChanges();

			IReviewRepository reviewRepository = new EFReviewRepository(context, null);

			var target = new ReviewController(reviewRepository);

			var result = target.Index();
			var model = result.Model as IEnumerable<ReviewViewModel>;

			Assert.NotNull(model);
			Assert.Equal("TestReview", model.FirstOrDefault().Name);
		}

		[Fact]
		public void TestAddReview()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);

			var accommodation = new Accommodation()
			{
				Id = 1,
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

			var target = new ReviewController(reviewRepository)
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
			Assert.NotNull(result.ActionName);
			Assert.NotNull(result.ControllerName);
			Assert.Equal("Detail", result.ActionName);
			Assert.Equal("Accommodation", result.ControllerName);
		}
	}
}
