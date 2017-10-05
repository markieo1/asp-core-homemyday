using HomeMyDay.Database.Identity;
using HomeMyDay.Extensions;
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
	[Authorize(Policy = IdentityPolicies.Administrator)]
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
		public IActionResult Edit(long id)
		{
			Accommodation accommodation;

			try
			{
				if (id <= 0)
				{
					accommodation = new Accommodation();
				}
				else
				{
					accommodation = _accommodationRepository.GetAccommodation(id);
				}
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
		public async Task<IActionResult> Edit(long id, Accommodation accommodation)
		{
			if (ModelState.IsValid)
			{
				bool saved = await _accommodationRepository.Save(id, accommodation);

				if (saved)
				{
					return RedirectToAction(
						actionName: nameof(Index),
						controllerName: nameof(AccommodationController).TrimControllerName());
				}
				else
				{
					return new StatusCodeResult(500);
				}
			}

			return View(accommodation);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			bool deleted = await _accommodationRepository.Delete(id);

			if (deleted)
			{
				return RedirectToAction(
							actionName: nameof(Index),
							controllerName: nameof(AccommodationController).TrimControllerName());
			}

			return new StatusCodeResult(500);
		}
	}
}
