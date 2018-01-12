using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Authorization;
using HomeMyDay.Core.Extensions;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Site.Cms.Controllers
{
	[Area("CMS")]
	[Authorize(Policy = Policies.Administrator)]
	public class AccommodationController : Controller
	{
		private readonly IAccommodationManager _accommodationManager;

		public AccommodationController(IAccommodationManager accommodationManager)
		{
			_accommodationManager = accommodationManager;
		}
				
		[HttpGet]
		public async Task<IActionResult> Index(int? page, int? pageSize)
		{
			return View(await _accommodationManager.GetAccommodationPaginatedList(page, pageSize));
		}

		[HttpGet]
		public IActionResult Edit(string id)
		{								
			try
			{
				return View(_accommodationManager.GetAccommodation(id));
			}
			catch (KeyNotFoundException)
			{
				return new NotFoundResult();
			}	
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Accommodation accommodation)
		{
			if (ModelState.IsValid)
			{
				await _accommodationManager.Save(accommodation);

				return RedirectToAction(
					actionName: nameof(Index),
					controllerName: nameof(AccommodationController).TrimControllerName());
			}

			return View(accommodation);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			await _accommodationManager.Delete(id);

			return RedirectToAction(
						actionName: nameof(Index),
						controllerName: nameof(AccommodationController).TrimControllerName());
		}
	}
}
