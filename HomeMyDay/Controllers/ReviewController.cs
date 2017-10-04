using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Controllers
{
	public class ReviewController : Controller
    {
        private readonly IReviewRepository _repository ;

        public ReviewController(IReviewRepository repo)
        {
            _repository = repo;           
        }

        public ViewResult Index()
        {
            var reviews = _repository.Reviews;
            return View(reviews);
        }

		[HttpGet]
	    public ViewResult Review()
	    {	   
		    return View();
	    }

	    [HttpPost]
	    public ViewResult Review(ReviewViewModel reviewViewModel)
	    {
		    if (_repository.AddReview(reviewViewModel.AccommodationId, reviewViewModel.Title, 
				reviewViewModel.Name, reviewViewModel.Text))
		    {
			    ViewData["Succeeded"] = "true";
			    return View();
		    }

		    ViewData["Succeeded"] = "false";
			return View();
	    }
    }
}