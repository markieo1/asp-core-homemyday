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
        private readonly IHolidayRepository _repository ;

        public ReviewController(IHolidayRepository repo)
        {
            _repository = repo;           
        }

        public ViewResult Index(ReviewViewModel model)
        {
            var reviews = _repository.Reviews;
            if(!reviews.Any())
            {
                return null;
            }
            else
            {
                return View("index", reviews.OrderByDescending(a => a.Id));
            }
        }
    }
}
