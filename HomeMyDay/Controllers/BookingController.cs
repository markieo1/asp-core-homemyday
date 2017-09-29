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
		private IAccommodationRepository repository;
		private ICountryRepository countryRepository;

		public BookingController(IAccommodationRepository repo, ICountryRepository countryRepo)
		{
			this.repository = repo;
			this.countryRepository = countryRepo;
		}

		[HttpGet]
        public IActionResult BookingForm(int accommodation)
        {
			var formModel = new BookingFormViewModel();

			formModel.Accommodation = repository.GetAccommodation(accommodation);

			if(formModel.Accommodation == null)
			{
				return RedirectToAction("Error", "Home");
			}

			formModel.Persons = new List<BookingPerson>();

			//Initialize empty BookingPersons
			for(int i = 0; i < formModel.Accommodation.MaxPersons; i++)
			{
				formModel.Persons.Add(new BookingPerson());
			}

			//Get countries from db
			List<Country> countries = countryRepository.Countries.OrderBy(c => c.Name).ToList();
			ViewBag.Countries = countries;

            return View(formModel);
        }

		[HttpPost]
		public IActionResult BookingForm(BookingFormViewModel formData)
		{
			if(!ModelState.IsValid)
			{
				return View();
			}
			else
			{
				//Get accommodation ID
				long accommodationId = formData.Accommodation.Id;

				//Store model in Session
				HttpContext.Session.Set("booking", new Booking() {
					Accommodation = repository.GetAccommodation(accommodationId),
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
				return View();
			}
			else
			{
				Booking booking = HttpContext.Session.Get<Booking>("booking");

				booking.InsuranceCancellationBasic = formModel.InsuranceCancellationBasic;
				booking.InsuranceCancellationAllRisk = formModel.InsuranceCancellationAllRisk;
				booking.InsuranceService = formModel.InsuranceService;
				booking.InsuranceExplore = formModel.InsuranceExplore;
				booking.InsuranceType = formModel.InsuranceType;
				booking.TransferFromAirport = formModel.TransferFromAirport;
				booking.TransferToAirport = formModel.TransferToAirport;

				HttpContext.Session.Set("booking", booking);

				return RedirectToAction("Confirmation");
			}
		}

		[HttpGet]
		public IActionResult Confirmation()
		{
			//Retrieve booking from TempData
			Booking booking = HttpContext.Session.Get<Booking>("booking");

			return View(booking);
		}
    }
}