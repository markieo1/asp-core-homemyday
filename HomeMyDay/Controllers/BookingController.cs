using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult BookingForm()
        {
            return View();
        }
    }
}