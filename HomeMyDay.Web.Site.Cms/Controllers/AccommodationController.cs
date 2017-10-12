using HomeMyDay.Core.Extensions;
using HomeMyDay.Core;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Infrastructure.Database;

namespace HomeMyDay.Web.Controllers.Cms
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
		public async Task<IActionResult> Edit(Accommodation accommodation)
		{
			if (ModelState.IsValid)
			{
				await _accommodationRepository.Save(accommodation);

				return RedirectToAction(
					actionName: nameof(Index),
					controllerName: nameof(AccommodationController).TrimControllerName());
			}

			return View(accommodation);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			await _accommodationRepository.Delete(id);

			return RedirectToAction(
						actionName: nameof(Index),
						controllerName: nameof(AccommodationController).TrimControllerName());
		}
	}
}
