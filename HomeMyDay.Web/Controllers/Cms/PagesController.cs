using HomeMyDay.Web.Database.Identity;
using HomeMyDay.Web.Helpers;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Controllers.Cms
{
	[Area("CMS")]
	[Authorize(Policy = IdentityPolicies.Administrator)]
	public class PagesController : Controller
	{
		private readonly IPageRepository _pageRepository;

		public PagesController(IPageRepository pageRepository)
		{
			_pageRepository = pageRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int? page, int? pageSize)
		{
				PaginatedList<Page> paginatedResult = await _pageRepository.List(page ?? 1, pageSize ?? 5);
				return View(paginatedResult);
		}

		[HttpGet]
		public IActionResult Edit(long id)
		{
			Page _surprise = _pageRepository.GetPage(id);
			PageViewModel model = new PageViewModel() { Title = _surprise.Title, Content = _surprise.Content };

			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(long id, Page page)
		{
			Page _surprise = _pageRepository.GetPage(id);
			if (_surprise != null)
			{
				_pageRepository.EditPage(id, page);
				return View();
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Error, something went wrong while editing");
				return View();
			}
		}
	}
}
