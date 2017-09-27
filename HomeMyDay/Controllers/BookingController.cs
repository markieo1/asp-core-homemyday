using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.ViewModels;
using HomeMyDay.Repository;
using HomeMyDay.Models;

namespace HomeMyDay.Controllers
{
    public class BookingController : Controller
    {
		private IHolidayRepository repository;

		public BookingController(IHolidayRepository repo)
		{
			this.repository = repo;
		}

		[HttpGet]
        public IActionResult BookingForm(int accommodation)
        {
			var formModel = new BookingFormViewModel();

			//TODO: move this to a repository method
			formModel.Accommodation = repository.Accommodations.Where(a => a.Id == accommodation).First();

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
		public IActionResult BookingForm(BookingFormViewModel formModel)
		{
			return View();
		}
    }
}