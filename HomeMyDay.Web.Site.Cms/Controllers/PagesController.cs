using System;
using System.Threading.Tasks;
using HomeMyDay.Core.Authorization;
using HomeMyDay.Core.Models;
using HomeMyDay.Web.Base.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Cms.Controllers
{
	[Area("CMS")]
	[Authorize(Policy = Policies.Administrator)]
	public class PagesController : Controller
	{		   
		private readonly IPageManager _pageManager;

		public PagesController(IPageManager pageManager)
		{
			_pageManager = pageManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int? page, int? pageSize)
		{
			return View(await _pageManager.GetPagePaginatedList(page, pageSize));
		}

		[HttpGet]
		public IActionResult Edit(long id)
		{
			return View(_pageManager.GetPageViewModel(id));
		}

		[HttpPost]
		public IActionResult Edit(long id, Page page)
		{
			try
			{
				_pageManager.EditPage(id, page);
				return View();
			}  
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Error, something went wrong while editing");
				return View();
			}	
		}
	}
}
