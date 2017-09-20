using HomeMyDay.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Components
{
    public class HolidayComponent : ViewComponent
    {
        private IEnumerable<Holiday> model;
        private IRepositoryHoliday repository;
        public string Destination { get; set; }

        class Holiday
        {
            public string Destination { get; set; }
            public string Place { get; set; }
            public bool Recommended { get; set; }
            public int Price { get; set; }
        }

        private List<Holiday> fakeHolidays = new List<Holiday>
        {
            new Holiday {Destination = "Netherlands", Place = "Rotterdam", Recommended = true, Price = 128},
            new Holiday {Destination = "Netherlands", Place = "Amsterdam", Recommended = false, Price = 148},
        };

        public HolidayComponent(IRepositoryHoliday repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult ShowRecommendedHoliday(int amount)
        {
            model = (IEnumerable<Holiday>) fakeHolidays;

            model.Where(m => m.Recommended == true);

            foreach (var m in model)
            {
                Destination = m.Destination;
            };

            return View("RecommendedHoliday", this);
        }
    }
}
