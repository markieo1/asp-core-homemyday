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

namespace HomeMyDay.Tests
{
    public class BookingControllerTest
    {
		/// <summary>
		/// Expected result: 
		/// </summary>
		[Fact]
		public void TestBookingFormBadRequest()
		{
			//Setup fake non-existent accommodation
			Accommodation accommodation = null;

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

			//Setup controller
			var controller = new BookingController(accommodationRepo.Object, countryRepo.Object, googleOpts.Object);
			IActionResult result = controller.BookingForm(1);

			//Test if controller returned a BadRequestResult.
			Assert.IsType<BadRequestResult>(result);
		}

		[Fact]
		public void TestBookingFormGet()
		{

		}
    }
}
