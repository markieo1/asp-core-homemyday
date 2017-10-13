using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Core.Extensions;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Web.Base.ViewModels;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class ReviewController : Controller
	{
		private readonly IReviewManager _reviewManager;

		public ReviewController(IReviewManager reviewManager)
		{
			_reviewManager = reviewManager;
		}

		public ViewResult Index()
		{
			return View(_reviewManager.GetReviews());
		}

		[HttpPost]
		public IActionResult AddReview(ReviewViewModel reviewViewModel)
		{
			if (_reviewManager.AddReview(reviewViewModel.AccommodationId, reviewViewModel.Title,
				reviewViewModel.Name, reviewViewModel.Text))
			{
				TempData["Succeeded"] = true;
				return RedirectToAction(nameof(AccommodationController.Detail), nameof(AccommodationController).TrimControllerName(), new { id = reviewViewModel.AccommodationId });
			}

			TempData["Succeeded"] = false;
			return RedirectToAction(nameof(AccommodationController.Detail), nameof(AccommodationController).TrimControllerName());
		}
	}
}