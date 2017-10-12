using HomeMyDay.Core.Models;
using HomeMyDay.Web.Home.Repository;
using HomeMyDay.Web.Home.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Home.Components
{
	public class SurprisePopUpViewComponent : ViewComponent
	{
		private readonly IPageRepository _surpriseRepository;

		public SurprisePopUpViewComponent(IPageRepository repository)
		{
			_surpriseRepository = repository;
		}

		public IViewComponentResult Invoke()
		{
			Page _surprise = _surpriseRepository.GetPage(1);
			if (_surprise != null)
			{
				PageViewModel model = new PageViewModel() { Title = _surprise.Title, Content = _surprise.Content };
				
				return View(model);
			}
			else
			{
				return View("NoSurprise");
			}
		}
	}
}
