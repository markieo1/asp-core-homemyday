using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.ViewModels;
using HomeMyDay.Repository;
using HomeMyDay.Models;
using HomeMyDay.Extensions;

namespace HomeMyDay.Controllers
{
	public class BookingController : Controller
	{
		private const string BOOKINGSESSIONKEY = "booking";

		private readonly IAccommodationRepository accommodationRepository;
		private readonly ICountryRepository countryRepository;

		public BookingController(IAccommodationRepository repo, ICountryRepository countryRepo)
		{
			this.accommodationRepository = repo;
			this.countryRepository = countryRepo;
		}

		[HttpGet]
		public IActionResult BookingForm(int id)
		{
			var formModel = new BookingFormViewModel();

			formModel.Accommodation = accommodationRepository.GetAccommodation(id);

			if(formModel.Accommodation == null)
			{
				return BadRequest();
			}

			formModel.Persons = new List<BookingPerson>();

			//Initialize empty BookingPersons
			for(int i = 0; i < formModel.Accommodation.MaxPersons; i++)
			{
				formModel.Persons.Add(new BookingPerson());
			}

			//Get countries from db
			IEnumerable<Country> countries = countryRepository.Countries.OrderBy(c => c.Name);
			ViewBag.Countries = countries;

			return View(formModel);
		}

		[HttpPost]
		public IActionResult BookingForm(BookingFormViewModel formData)
		{
			if(!ModelState.IsValid)
			{
				formData.Accommodation = accommodationRepository.GetAccommodation(formData.Accommodation.Id);
				ViewBag.Countries = countryRepository.Countries.OrderBy(c => c.Name);

				return View(formData);
			}
			else
			{
				//Get accommodation ID
				long accommodationId = formData.Accommodation.Id;

				//Get country ID
				foreach(BookingPerson person in formData.Persons)
				{
					person.Country = countryRepository.GetCountry(person.Country.Id);
					person.Nationality = countryRepository.GetCountry(person.Nationality.Id);
				}

				//Store model in Session
				HttpContext.Session.Set(BOOKINGSESSIONKEY, new Booking() {
					Accommodation = accommodationRepository.GetAccommodation(accommodationId),
					Persons = formData.Persons,
				});

				return RedirectToAction("InsuranceForm");
			}
		}

		[HttpGet]
		public IActionResult InsuranceForm()
		{
			//Retrieve booking from Session
			InsuranceFormViewModel formModel = new InsuranceFormViewModel();

			return View(formModel);
		}

		[HttpPost]
		public IActionResult InsuranceForm(InsuranceFormViewModel formModel)
		{
			if(!ModelState.IsValid)
			{
				return View(formModel);
			}
			else
			{
				Booking booking = HttpContext.Session.Get<Booking>(BOOKINGSESSIONKEY);

				booking.InsuranceCancellationBasic = formModel.InsuranceCancellationBasic;
				booking.InsuranceCancellationAllRisk = formModel.InsuranceCancellationAllRisk;
				booking.InsuranceService = formModel.InsuranceService;
				booking.InsuranceExplore = formModel.InsuranceExplore;
				booking.InsuranceType = formModel.InsuranceType;
				booking.TransferFromAirport = formModel.TransferFromAirport;
				booking.TransferToAirport = formModel.TransferToAirport;

				HttpContext.Session.Set(BOOKINGSESSIONKEY, booking);

				return RedirectToAction("Confirmation");
			}
		}

		[HttpGet]
		public IActionResult Confirmation()
		{
			//Retrieve booking from TempData
			Booking booking = HttpContext.Session.Get<Booking>(BOOKINGSESSIONKEY);

			return View(booking);
		}
	}
}