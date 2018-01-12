using HomeMyDay.Core.Services;
using HomeMyDay.Web.Base.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class AccommodationController : Controller
	{
		private readonly IAccommodationManager _accommodationManager;
		private readonly IMapService _mapService;

		public AccommodationController(IAccommodationManager accommodationManager, IMapService mapService)
		{
			_accommodationManager = accommodationManager;
			_mapService = mapService;
		}

		[HttpGet]
		public IActionResult Detail(string id)
		{
			try
			{
				ViewBag.MapApiKey = _mapService.GetApiKey();
				return View(_accommodationManager.GetAccommodationViewModel(id));
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
		}
	}
}
