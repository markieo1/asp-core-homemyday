using HomeMyDay.Models;
using HomeMyDay.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Components
{
    public class RecommendedHoliday : ViewComponent
    {
        private IHolidayRepository repository;

        public RecommendedHoliday(IHolidayRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Holiday> model = repository.Holidays.Where(m => m.Recommended == true);

            return View(model);
        }
    }
}
