using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.ViewModels;
using HomeMyDay.Repository;
using HomeMyDay.Models;
using HomeMyDay.Extensions;
using HomeMyDay.Services;
using Microsoft.Extensions.Options;

namespace HomeMyDay.Controllers
{
	public class BookingController : Controller
	{
		private const string BOOKINGSESSIONKEY = "booking";

		private readonly IAccommodationRepository accommodationRepository;
		private readonly ICountryRepository countryRepository;
		private readonly GoogleApiServiceOptions googleOptions;

		public BookingController(IAccommodationRepository repo, ICountryRepository countryRepo, IOptions<GoogleApiServiceOptions> googleOpts)
		{
			this.accommodationRepository = repo;
			this.countryRepository = countryRepo;
			this.googleOptions = googleOpts.Value;
		}

		[HttpGet]
		public IActionResult BookingForm(int id, int? persons)
		{
			var formModel = new BookingFormViewModel();

			try
			{
				formModel.Accommodation = accommodationRepository.GetAccommodation(id);
			}
			catch(KeyNotFoundException)
			{
				return BadRequest();
			}

			formModel.Persons = new List<BookingPerson>();

			//If an amount of persons was given, use it.
			//Otherwise, get the maximum persons from the Accommodation.
			int maxPersons;
			if(persons.HasValue && persons.Value <= formModel.Accommodation.MaxPersons)
			{
				maxPersons = persons.Value;
			}
			else
			{
				maxPersons = formModel.Accommodation.MaxPersons;
			}

			//Initialize BookingPersons for the form
			for(int i = 0; i < maxPersons; i++)
			{
				formModel.Persons.Add(new BookingPerson());
			}

			//Get countries from db
			IEnumerable<Country> countries = countryRepository.Countries.OrderBy(c => c.Name);
			ViewBag.Countries = countries;

			//Get google client API key
			ViewBag.GoogleClientApiKey = googleOptions.ClientApiKey;

			return View("BookingForm", formModel);
		}

		[HttpPost]
		public IActionResult BookingForm(BookingFormViewModel formData)
		{
			if(!ModelState.IsValid)
			{
				formData.Accommodation = accommodationRepository.GetAccommodation(formData.Accommodation.Id);
				ViewBag.Countries = countryRepository.Countries.OrderBy(c => c.Name);

				return View("BookingForm", formData);
			}
			else
			{
				//Get accommodation from posted ID
				Accommodation accommodation;
				try
				{
					accommodation = accommodationRepository.GetAccommodation(formData.Accommodation.Id);
				}
				catch(KeyNotFoundException)
				{
					return BadRequest();
				}

				//Get country and nationality objects from ID
				foreach(BookingPerson person in formData.Persons)
				{
					person.Country = countryRepository.GetCountry(person.Country.Id);
					person.Nationality = countryRepository.GetCountry(person.Nationality.Id);
				}

				//Store model in Session
				HttpContext.Session.Set(BOOKINGSESSIONKEY, new Booking() {
					Accommodation = accommodation,
					Persons = formData.Persons,
				});

				return RedirectToAction("InsuranceForm");
			}
		}

		[HttpGet]
		public ViewResult InsuranceForm()
		{
			//Retrieve booking from Session
			InsuranceFormViewModel formModel = new InsuranceFormViewModel();

			return View("InsuranceForm", formModel);
		}

		[HttpPost]
		public IActionResult InsuranceForm(InsuranceFormViewModel formModel)
		{
			if(!ModelState.IsValid)
			{
				return View("InsuranceForm", formModel);
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

			return View("Confirmation", booking);
		}
	}
}