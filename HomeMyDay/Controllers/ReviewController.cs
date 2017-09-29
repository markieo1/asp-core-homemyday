using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HomeMyDay.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _repository ;

        public ReviewController(IReviewRepository repo)
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
