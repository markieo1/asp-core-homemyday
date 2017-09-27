using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Controllers
{
    public class ReviewController : Controller
    {
        private IHolidayRepository _IHolidayRepository;

        public ReviewController(IHolidayRepository repo)
        {
            _IHolidayRepository = repo;           
        }

        public ViewResult Index(ReviewViewModel model)
        {
            var reviews = _IHolidayRepository.Reviews;
            return View("index", reviews.OrderByDescending(a => a.Id));
        }
    }
}
