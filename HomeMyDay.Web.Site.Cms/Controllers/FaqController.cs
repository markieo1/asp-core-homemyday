using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMyDay.Core.Authorization;
using HomeMyDay.Core.Extensions;
using HomeMyDay.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Web.Base.ViewModels;

namespace HomeMyDay.Web.Site.Cms.Controllers
{
	[Area("CMS")]
	[Authorize(Policy = Policies.Administrator)]
	public class FaqController : Controller
	{
		private readonly IFaqManager _faqManager;

		public FaqController(IFaqManager faqManager)
		{
			_faqManager = faqManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int? page, int? pageSize)
		{
			return View(await _faqManager.GetFaqCategoryPaginatedList(page, pageSize));
		}

		[HttpGet]
		public async Task<IActionResult> Questions(long id, int? page, int? pageSize)
		{
			return View(await _faqManager.GetFaqQuestionsViewModel(id, page, pageSize));
		}

		[HttpGet]
		public IActionResult EditCategory(long id)
		{
			try
			{
				return View(_faqManager.GetFaqCategory(id));
			}
			catch (KeyNotFoundException)
			{
				return new NotFoundResult();
			}
		}

		[HttpPost]
		public async Task<IActionResult> EditCategory(FaqCategory cat)
		{
			if (ModelState.IsValid)
			{
				await _faqManager.SaveCategory(cat);

				return RedirectToAction(
					actionName: nameof(Index),
					controllerName: nameof(FaqController).TrimControllerName());
			}

			return View(cat);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteCategory(long id)
		{
			await _faqManager.DeleteCategory(id);

			return RedirectToAction(
						actionName: nameof(Index),
						controllerName: nameof(FaqController).TrimControllerName());
		}

		/// <summary>
		/// Edits the question.
		/// </summary>
		/// <param name="id">The category identifier.</param>
		/// <param name="questionId">The question identifier.</param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult EditQuestion(long id, long questionId)
		{
			try
			{
				return View(_faqManager.GetFaqQuestionEditViewModel(id, questionId));
			}
			catch (KeyNotFoundException)
			{
				return new NotFoundResult();
			}
		}

		[HttpPost]
		public async Task<IActionResult> EditQuestion(FaqQuestionEditViewModel faqQuestionEditViewModel)
		{
			if (ModelState.IsValid)
			{
				FaqQuestion faqQuestion = new FaqQuestion()
				{
					Id = faqQuestionEditViewModel.QuestionId,
					Question = faqQuestionEditViewModel.Question,
					Answer = faqQuestionEditViewModel.Answer,
					Category = _faqManager.GetFaqCategory(faqQuestionEditViewModel.Id)
				};

				await _faqManager.SaveQuestion(faqQuestion);

				return RedirectToAction(
					actionName: nameof(Questions),
					controllerName: nameof(FaqController).TrimControllerName(),
					routeValues: new
					{
						id = faqQuestion.Category.Id
					});
			}

			return View(faqQuestionEditViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteQuestion(long id, long categoryId)
		{
			await _faqManager.DeleteQuestion(id);

			return RedirectToAction(
					actionName: nameof(Questions),
					controllerName: nameof(FaqController).TrimControllerName(),
					routeValues: new
					{
						id = categoryId
					});
		}
	}
}
