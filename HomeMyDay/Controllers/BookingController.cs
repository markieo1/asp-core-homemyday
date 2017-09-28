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

		public BookingController(IAccommodationRepository repo)
		{
			this.repository = repo;
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
				//Store model in TempData
				TempData.Put("booking", new Booking() {
					Accommodation = formData.Accommodation,
					Persons = formData.Persons,
				});

				Console.WriteLine("Redirecting to InsuranceForm");

				return RedirectToAction("InsuranceForm");
			}
		}

		[HttpGet]
		public IActionResult InsuranceForm()
		{
			//Retrieve booking from TempData
			//Booking booking = TempData.Get<Booking>("booking");

			//TODO: remove empty booking for testing
			Booking booking = new Booking() {
				Accommodation = repository.GetAccommodation(1),
				Persons = new List<BookingPerson>() {
					new BookingPerson(),
					new BookingPerson()
				}
			};

			if(booking == null)
			{
				return RedirectToAction("Error", "Home");
			}

			return View(booking);
		}
    }
}