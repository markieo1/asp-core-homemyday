using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMyDay.Core.Authorization;
using HomeMyDay.Core.Extensions;
using HomeMyDay.Core.Models;
using HomeMyDay.Web.Base.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Cms.Controllers
{
	[Area("CMS")]
	[Authorize(Policy = Policies.Administrator)]
	public class VacancyController : Controller
	{
		private readonly IVacancyManager _vacancyManager;

		public VacancyController(IVacancyManager vacancyManager)
		{
			_vacancyManager = vacancyManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int? page, int? pageSize)
		{
			return View(_vacancyManager.GetVacancyPaginatedList(page, pageSize));
		}

		[HttpGet]
		public ViewResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(Vacancy vacancy)
		{
			if (ModelState.IsValid)
			{
				_vacancyManager.Save(vacancy);
				return RedirectToAction(nameof(Index));
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(long id)
		{
			await _vacancyManager.Delete(id);

			return RedirectToAction(
						actionName: nameof(Index),
						controllerName: nameof(VacancyController).TrimControllerName());
		}

		[HttpGet]
		public IActionResult Edit(long id)
		{
			try
			{
				return View(_vacancyManager.GetVacancy(id));
			}
			catch (KeyNotFoundException)
			{
				return new NotFoundResult();
			}
			catch (ArgumentOutOfRangeException)
			{
				return new BadRequestResult();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Vacancy vacancy)
		{
			if (ModelState.IsValid)
			{
				try
				{  
					await _vacancyManager.Save(vacancy);
					return RedirectToAction(
						actionName: nameof(Index),
						controllerName: nameof(VacancyController).TrimControllerName());
				}
				catch (Exception)
				{
					ModelState.AddModelError(string.Empty, "Error, something went wrong while saving vacancy.");
				}
			}																						 

			return View(vacancy);
		}
	}
}
