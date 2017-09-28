using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Components
{
	public class PopupLoginViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			LoginViewModel model = new LoginViewModel();
			return View(model);
		}
	}
}
