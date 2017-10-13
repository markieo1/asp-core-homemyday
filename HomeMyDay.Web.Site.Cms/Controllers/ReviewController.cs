using System;
using System.Threading.Tasks;
using HomeMyDay.Core.Authorization;
using HomeMyDay.Web.Base.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Cms.Controllers
{
	[Area("CMS")]
    [Authorize(Policy = Policies.Administrator)]
    public class ReviewController : Controller
	{
		private readonly IReviewManager _reviewManager;

	    public ReviewController(IReviewManager reviewManager)
	    {
		    _reviewManager = reviewManager;
	    }

        [HttpGet]
        public async Task<IActionResult> Index(int? page, int? pageSize)
        {
	        return View(await _reviewManager.GetPaginatedReview(page, pageSize));
        }

        [HttpPost]
        public IActionResult Accept(long id)
        {
	        try
	        {
		        _reviewManager.AcceptReview(id);
		        return RedirectToAction(nameof(Index));
			}
	        catch (Exception)
	        {
		        ModelState.AddModelError(string.Empty, "Error, something went wrong while accepting review.");
				return View();
	        }  
        }
    }
}
