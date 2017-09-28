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
			return View();
		}
    }
}
