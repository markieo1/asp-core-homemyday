using HomeMyDay.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Core.Extensions;
using HomeMyDay.Web.Base.BaseControllers;
using HomeMyDay.Web.Base.ViewModels;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class ReviewController : ReviewBaseController
	{													
		public ReviewController(IReviewRepository repository)
			:base(repository)
		{					   

		}

		public ViewResult Index()
		{
			return View(GetReviews());
		}

		[HttpPost]
		public IActionResult AddReview(ReviewViewModel reviewViewModel)
		{
			if (AddReview(reviewViewModel.AccommodationId, reviewViewModel.Title,
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