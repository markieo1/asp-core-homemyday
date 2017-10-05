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
			//Mock accommodation repo
			var accommodationRepo = new Mock<IAccommodationRepository>();
			if(shouldHaveAccommodations)
			{
				//Setup fake accommodation
				var accommodation = new Accommodation()
				{
					Id = 1,
					MaxPersons = 4,
					Name = "Test Accommodation"
				};

				accommodationRepo.Setup(r => r.GetAccommodation(It.IsAny<long>())).Returns(accommodation);
			}
			else
			{
				//If there are no accommodations, always throw a KeyNotFoundException
				accommodationRepo.Setup(r => r.GetAccommodation(It.IsAny<long>())).Throws(new KeyNotFoundException());
			}
			
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

			byte[] emptyJsonObjectString = Encoding.ASCII.GetBytes("{}");
			sessionMock.Setup(s => s.TryGetValue(It.IsAny<string>(), out emptyJsonObjectString));
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
			//Initialize controller and excute action.
			//No accommodations are in the repository.
			BookingController controller = GetController(false);
			IActionResult result = controller.BookingForm(1, null);

			//Test if controller returned a BadRequest.
			Assert.IsType<BadRequestResult>(result);
		}

		[Fact]
		public void TestBookingFormSuccessfulGet()
		{
			//Initialize controller and execute action
			//There are accommodations in the repository.
			BookingController controller = GetController(true);

			ViewResult result = controller.BookingForm(1, null) as ViewResult;
			
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
		public void TestBookingFormPostNonExistentAccommodation()
		{
			BookingController controller = GetController(false);

			//Fake post data with a non-existent accommodation
			var formModel = new BookingFormViewModel()
			{
				Accommodation = new Accommodation()
				{
					Id = 333,
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

			Assert.IsType<BadRequestResult>(result);
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

			RedirectToActionResult result = controller.BookingForm(formModel) as RedirectToActionResult;

			Assert.Equal("InsuranceForm", result.ActionName);
		}

		[Fact]
		public void TestInsuranceFormSuccessfulGet()
		{
			BookingController controller = GetController(true);
			ViewResult result = controller.InsuranceForm();

			Assert.Equal("InsuranceForm", result.ViewName);
		}

		[Fact]
		public void TestInsuranceFormBadPost()
		{
			var viewModel = new InsuranceFormViewModel();

			BookingController controller = GetController(true);
			controller.ModelState.AddModelError("test", "Test Error");
			ViewResult result = controller.InsuranceForm(viewModel) as ViewResult;

			Assert.Equal("InsuranceForm", result.ViewName);
		}

		[Fact]
		public void TestInsuranceFormSuccessfulPost()
		{
			var viewModel = new InsuranceFormViewModel();

			BookingController controller = GetController(true);
			RedirectToActionResult result = controller.InsuranceForm(viewModel) as RedirectToActionResult;

			Assert.Equal("Confirmation", result.ActionName);
		}

		[Fact]
		public void TestConfirmationSuccessfulGet()
		{
			BookingController controller = GetController(true);
			ViewResult result = controller.Confirmation() as ViewResult;

			Assert.Equal("Confirmation", result.ViewName);
		}
	}
}
