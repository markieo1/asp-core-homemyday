using HomeMyDay.Database.Identity;
using HomeMyDay.Helpers;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Controllers.Cms
{
	[Area("CMS")]
	//[Authorize(Policy = IdentityPolicies.Administrator)]
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
			Page _suprise = _pageRepository.GetPage(id);
			PageViewModel model = new PageViewModel() { Title = _suprise.Title, Content = _suprise.Content };

			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(long id, Page page)
		{
			Page _suprise = _pageRepository.GetPage(id);
			if (_suprise != null)
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
