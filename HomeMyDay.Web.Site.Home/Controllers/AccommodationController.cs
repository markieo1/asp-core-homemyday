using HomeMyDay.Web.Base.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class AccommodationController : Controller
	{
		private readonly IAccommodationManager _accommodationManager;
				
		public AccommodationController(IAccommodationManager accommodationManager)
		{
			_accommodationManager = accommodationManager;
		}

		[HttpGet]
		public IActionResult Detail(long id)
		{  
			try
			{
				ViewBag.GoogleClientApiKey = _accommodationManager.GetClientApiKey();
				return View(_accommodationManager.GetAccommodationViewModel(id));
			}  
			catch (KeyNotFoundException)
			{
				return NotFound();
			}	
		}
	}
}
