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
		private readonly ISupriseRepository _supriseRepository;

		public SuprisePopUpViewComponent(ISupriseRepository repository)
		{
			_supriseRepository = repository;
		}

		public IViewComponentResult Invoke()
		{
			Suprise _suprise = _supriseRepository.GetLastSuprise();
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
