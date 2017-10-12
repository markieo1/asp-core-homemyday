using System.Threading.Tasks;
using HomeMyDay.Web.Database.Identity;
using HomeMyDay.Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Core.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using HomeMyDay.Web.Extensions;
using HomeMyDay.Web.Helpers;
using HomeMyDay.Web.ViewModels;

namespace HomeMyDay.Web.Controllers.Cms
{
	[Area("CMS")]
	[Authorize(Policy = IdentityPolicies.Administrator)]
	public class FaqController : Controller
	{
		private readonly IFaqRepository _faqRepository;

		public FaqController(IFaqRepository repository)
		{
			_faqRepository = repository;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int? page, int? pageSize)
		{
			var paginatedResult = await _faqRepository.ListCategories(page ?? 1, pageSize ?? 5);
			return View(paginatedResult);
		}

		[HttpGet]
		public async Task<IActionResult> Questions(long id, int? page, int? pageSize)
		{
			FaqCategory category = _faqRepository.GetCategory(id);
			PaginatedList<FaqQuestion> paginatedResult = await _faqRepository.ListQuestions(id, page ?? 1, pageSize ?? 5);

			FaqQuestionsViewModel viewModel = new FaqQuestionsViewModel()
			{
				Category = category,
				Questions = paginatedResult
			};

			return View(viewModel);
		}

		[HttpGet]
		public IActionResult EditCategory(long id)
		{

			FaqCategory category;

			try
			{
				if (id <= 0)
				{
					category = new FaqCategory();
				}
				else
				{
					category = _faqRepository.GetCategory(id);
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

			return View(category);
		}

		[HttpPost]
		public async Task<IActionResult> EditCategory(FaqCategory cat)
		{
			if (ModelState.IsValid)
			{
				await _faqRepository.SaveCategory(cat);

				return RedirectToAction(
					actionName: nameof(Index),
					controllerName: nameof(FaqController).TrimControllerName());
			}

			return View(cat);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteCategory(long id)
		{
			await _faqRepository.DeleteCategory(id);

			return RedirectToAction(
						actionName: nameof(Index),
						controllerName: nameof(FaqController).TrimControllerName());
		}

	}
}
