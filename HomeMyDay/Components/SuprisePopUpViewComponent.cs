using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Components
{
	public class SuprisePopUpViewComponent : ViewComponent
	{
		private readonly IPageRepository _supriseRepository;

		public SuprisePopUpViewComponent(IPageRepository repository)
		{
			_supriseRepository = repository;
		}

		public IViewComponentResult Invoke()
		{
			Page _suprise = _supriseRepository.GetSuprise();
			if (_suprise != null)
			{
				SuprisePopUpViewModel model = new SuprisePopUpViewModel() { Title = _suprise.Title, Content = _suprise.Content };
				return View(model);
			}
			else
			{
				return View("NoSuprise");
			}
		}
	}
}
