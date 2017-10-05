using System.Linq;
using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Controllers
{
	public class ReviewController : Controller
	{
		private readonly IReviewRepository _repository;

		public ReviewController(IReviewRepository repo)
		{
			_repository = repo;
		}

		public ViewResult Index()
		{
			return View(_repository.Reviews.Select(ReviewViewModel.FromReview).Where(x => x.Approved));
		}

		[HttpPost]
		public IActionResult AddReview(ReviewViewModel reviewViewModel)
		{
			if (_repository.AddReview(reviewViewModel.Accommodation, reviewViewModel.Title,
				reviewViewModel.Name, reviewViewModel.Text))
			{
				TempData["Succeeded"] = true;
				return RedirectToAction("Detail", "Accommodation", new { id = reviewViewModel.Accommodation.Id });
			}

			TempData["Succeeded"] = false;
			return RedirectToAction("Detail", "Accommodation");
		}
	}
}