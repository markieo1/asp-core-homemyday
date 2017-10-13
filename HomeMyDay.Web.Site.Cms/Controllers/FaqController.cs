using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMyDay.Core.Authorization;
using HomeMyDay.Core.Extensions;
using HomeMyDay.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;

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
			return View(await GetFaqQuestionsViewModel(id, page, pageSize)); 
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
	}
}
