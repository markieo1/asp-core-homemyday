using HomeMyDay.Models;
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
		public IViewComponentResult Invoke()
		{
			Suprise model = new Suprise();
			return View(model);
		}
		}
}
