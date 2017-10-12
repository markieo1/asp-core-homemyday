using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.ViewModels;
using HomeMyDay.Core.Repository;
using HomeMyDay.Core.Models;
using HomeMyDay.Web.Extensions;
using HomeMyDay.Web.Services;
using Microsoft.Extensions.Options;

namespace HomeMyDay.Web.Controllers
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

			Accommodation accommodation;
			try
			{
				accommodation = accommodationRepository.GetAccommodation(id);
				formModel.AccommodationId = accommodation.Id;
				formModel.AccommodationName = accommodation.Name;
			}
			catch(KeyNotFoundException)
			{
				return NotFound();
			}

			formModel.Persons = new List<BookingPerson>();

			//If an amount of persons was given, use it.
			//Otherwise, get the maximum persons from the Accommodation.
			int maxPersons;
			if(persons.HasValue && persons.Value <= accommodation.MaxPersons)
			{
				maxPersons = persons.Value;
			}
			else
			{
				maxPersons = accommodation.MaxPersons;
			}

			ViewBag.MaxPersons = maxPersons;

			//Initialize BookingPersons for the form
			for(int i = 0; i < accommodation.MaxPersons; i++)
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
				//Restore accommodation object from ID
				Accommodation accommodation;
				try
				{
					accommodation = accommodationRepository.GetAccommodation(formData.AccommodationId);
					formData.AccommodationName = accommodation.Name;
				}
				catch (KeyNotFoundException)
				{
					return NotFound();
				}

				ViewBag.Countries = countryRepository.Countries.OrderBy(c => c.Name);
				ViewBag.MaxPersons = formData.Persons.Count();

				//Initialize BookingPersons up to the maximum that the accommodation will support.
				//Old values entered by the user should be kept.
				for(int i = 0; i < accommodation.MaxPersons; i++)
				{
					if(formData.Persons.ElementAtOrDefault(i) == null)
					{
						formData.Persons.Add(new BookingPerson());
					}
				}

				return View("BookingForm", formData);
			}
			else
			{
				//Get accommodation from posted ID
				Accommodation accommodation;
				try
				{
					accommodation = accommodationRepository.GetAccommodation(formData.AccommodationId);
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