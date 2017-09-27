using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Controllers
{
	public class AccommodationController : Controller
	{
		[HttpGet]
		public IActionResult Detail(int id)
		{
			return View();
		}
	}
}
