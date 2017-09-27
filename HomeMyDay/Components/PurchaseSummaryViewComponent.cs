using HomeMyDay.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Components
{
    public class PurchaseSummaryViewComponent : ViewComponent
    {
		private IHolidayRepository repository;

		public PurchaseSummaryViewComponent(IHolidayRepository repository)
		{
			this.repository = repository;
		}

		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
