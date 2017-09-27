using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.ViewModels;

namespace HomeMyDay.Controllers
{
    public class BookingController : Controller
    {
		[HttpGet]
        public IActionResult BookingForm(int accommodation)
        {
			var formModel = new BookingFormViewModel();

            return View();
        }
    }
}