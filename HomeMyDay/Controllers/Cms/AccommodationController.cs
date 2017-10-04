using HomeMyDay.Helpers;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Controllers.Cms
{
	[Area("CMS")]
	public class AccommodationController : Controller
	{
		private readonly IAccommodationRepository _accommodationRepository;

		public AccommodationController(IAccommodationRepository accommodationRepository)
		{
			_accommodationRepository = accommodationRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int? page)
		{
			PaginatedList<Accommodation> paginatedResult = await _accommodationRepository.List(page ?? 1);
			return View(paginatedResult);
		}
	}
}
