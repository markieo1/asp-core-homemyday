using HomeMyDay.Web.Base.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class AccommodationController : Controller
	{
		private readonly IAccommodationManager _accommodationManager;
		private readonly IGoogleApiServiceOptionsManager _googleApiServiceOptionsManager;
				
		public AccommodationController(IAccommodationManager accommodationManager, IGoogleApiServiceOptionsManager googleApiServiceOptionsManager)
		{
			_accommodationManager = accommodationManager;
			_googleApiServiceOptionsManager = googleApiServiceOptionsManager;
		}

		[HttpGet]
		public IActionResult Detail(long id)
		{  
			try
			{
				ViewBag.GoogleClientApiKey = _googleApiServiceOptionsManager.GetClientApiKey();
				return View(_accommodationManager.GetAccommodationViewModel(id));
			}  
			catch (KeyNotFoundException)
			{
				return NotFound();
			}	
		}
	}
}
