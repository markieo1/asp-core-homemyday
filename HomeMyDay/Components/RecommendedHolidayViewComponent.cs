using HomeMyDay.Models;
using HomeMyDay.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Components
{
    public class RecommendedHolidayViewComponent : ViewComponent
    {
        private IHolidayRepository repository;

        public RecommendedHolidayViewComponent(IHolidayRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var model = repository.GetRecommendedHolidays();

            return View(model);
        }
    }
}
