using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using HomeMyDay.Controllers;
using HomeMyDay.Repository;
using HomeMyDay.Models;
using Microsoft.Extensions.Options;
using HomeMyDay.Services;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using HomeMyDay.Extensions;

namespace HomeMyDay.Tests
{
	public class BookingControllerTest
	{
		private BookingController GetController(bool shouldHaveAccommodations)
		{
			Accommodation accommodation = null;

			if(shouldHaveAccommodations)
			{
				//Setup fake accommodation
				accommodation = new Accommodation()
				{
					Id = 1,
					MaxPersons = 4,
					Name = "Test Accommodation"
				};
			}

			//Mock accommodation repo
			var accommodationRepo = new Mock<IAccommodationRepository>();
			accommodationRepo.Setup(r => r.GetAccommodation(It.IsAny<long>())).Returns(accommodation);

			//Setup fake countries
			var countries = new List<Country>() {
				new Country() { Id = 1, CountryCode = "NED", Name = "Netherlands", },
				new Country() { Id = 2, CountryCode = "USA", Name = "United States", },
				new Country() { Id = 3, CountryCode = "NOR", Name = "Norway", },
			};

			//Mock country repo
			var countryRepo = new Mock<ICountryRepository>();
			countryRepo.Setup(r => r.Countries).Returns(countries);

			//Setup fake google API options
			var fakeApiOptions = new GoogleApiServiceOptions()
			{
				ClientApiKey = "Testkey"
			};

			//Mock google api options
			var googleOpts = new Mock<IOptions<GoogleApiServiceOptions>>();
			googleOpts.Setup(g => g.Value).Returns(fakeApiOptions);

			var sessionMock = new Mock<ISession>();

			byte[] emptyString = Encoding.ASCII.GetBytes("{}");
			sessionMock.Setup(s => s.TryGetValue(It.IsAny<string>(), out emptyString));
			sessionMock.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()));

			//Set up a default HTTP context so the session can be mocked
			var httpContext = new DefaultHttpContext();
			httpContext.Session = sessionMock.Object;

			//Setup controller
			var controller = new BookingController(accommodationRepo.Object, countryRepo.Object, googleOpts.Object);

			controller.ControllerContext = new ControllerContext()
			{
				HttpContext = httpContext,
				RouteData = new RouteData()
			};

			return controller;
		}

		[Fact]
		public void TestBookingFormBadRequest()
		{
			//Initialize controller and excute action
			BookingController controller = GetController(false);
			IActionResult result = controller.BookingForm(1);

			//Test if controller returned a BadRequestResult.
			Assert.IsType<BadRequestResult>(result);
		}

		[Fact]
		public void TestBookingFormSuccessfulGet()
		{
			//Initialize controller and execute action
			BookingController controller = GetController(true);

			ViewResult result = controller.BookingForm(1) as ViewResult;
			
			BookingFormViewModel model = result.Model as BookingFormViewModel;

			Assert.Equal("BookingForm", result.ViewName);
			Assert.Equal(4, model.Accommodation.MaxPersons);
			Assert.Equal(4, model.Persons.Count);
		}

		[Fact]
		public void TestBookingFormBadPost()
		{
			BookingController controller = GetController(true);

			//Fake post data with missing attributes
			var formModel = new BookingFormViewModel()
			{
				Accommodation = new Accommodation()
				{
					Id = 1,
					MaxPersons = 4,
					Name = "Test Accommodation"
				},
				Persons = new List<BookingPerson>()
				{
					new BookingPerson()
					{
						Country = new Country()
						{
							Id = 1
						},
						Nationality = new Country()
						{
							Id = 2
						}
					}
				}
			};

			//Manually add error to model state
			controller.ModelState.AddModelError("test", "Test Error");
			ViewResult result = controller.BookingForm(formModel) as ViewResult;

			Assert.Equal("BookingForm", result.ViewName);
			Assert.False(controller.ModelState.IsValid);
		}

		[Fact]
		public void TestBookingFormSuccessfulPost()
		{
			BookingController controller = GetController(true);

			var formModel = new BookingFormViewModel()
			{
				Accommodation = new Accommodation()
				{
					Id = 1,
					MaxPersons = 4,
					Name = "Test Accommodation"
				},
				Persons = new List<BookingPerson>()
				{
					new BookingPerson()
					{
						Country = new Country()
						{
							Id = 1
						},
						Nationality = new Country()
						{
							Id = 2
						}
					}
				}
			};

			IActionResult result = controller.BookingForm(formModel);

			Assert.IsType<RedirectToActionResult>(result);

			
		}
	}
}
