using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMyDay.Core.Authorization;
using HomeMyDay.Core.Extensions;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.BaseControllers;

namespace HomeMyDay.Web.Site.Cms.Controllers
{
	[Area("CMS")]
	[Authorize(Policy = Policies.Administrator)]
	public class FaqController : FaqBaseController
	{
		public FaqController(IFaqRepository repository)
			: base(repository)
		{
			
		} 

		[HttpGet]
		public async Task<IActionResult> Index(int? page, int? pageSize)
		{
			return View(await GetFaqCategoryPaginatedList(page, pageSize));
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
				return View(GetFaqCategory(id));
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
				await SaveCategory(cat);			 

				return RedirectToAction(
					actionName: nameof(Index),
					controllerName: nameof(FaqController).TrimControllerName());
			}

			return View(cat);
		}

		[HttpPost]
		public new async Task<IActionResult> DeleteCategory(long id)
		{
			await base.DeleteCategory(id);

			return RedirectToAction(
						actionName: nameof(Index),
						controllerName: nameof(FaqController).TrimControllerName());
		}  
	}
}
