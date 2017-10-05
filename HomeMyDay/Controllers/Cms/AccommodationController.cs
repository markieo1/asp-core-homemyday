using HomeMyDay.Database.Identity;
using HomeMyDay.Helpers;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Controllers.Cms
{
	[Area("CMS")]
	//[Authorize(Policy = IdentityPolicies.Administrator)]
	public class AccommodationController : Controller
	{
		private readonly IAccommodationRepository _accommodationRepository;

		public AccommodationController(IAccommodationRepository accommodationRepository)
		{
			_accommodationRepository = accommodationRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int? page, int? pageSize)
		{
			PaginatedList<Accommodation> paginatedResult = await _accommodationRepository.List(page ?? 1, pageSize ?? 5);
			return View(paginatedResult);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			Accommodation accommodation;
			try
			{
				accommodation = _accommodationRepository.GetAccommodation(id);
			}
			catch (KeyNotFoundException)
			{
				return new NotFoundResult();
			}
			catch (ArgumentOutOfRangeException)
			{
				return new BadRequestResult();
			}

			return View(accommodation);
		}

		[HttpPost]
		public IActionResult Edit(int id, Accommodation accommodation)
		{
			return View();
		}
	}
}
