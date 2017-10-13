using System.Threading.Tasks;
using HomeMyDay.Core.Authorization;
using HomeMyDay.Core.Repository;
using HomeMyDay.Web.Base.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Cms.Controllers
{
	[Area("CMS")]
    [Authorize(Policy = Policies.Administrator)]
    public class ReviewController : ReviewBaseController
    {
	    public ReviewController(IReviewRepository repository)
		    : base(repository)
	    {
		    
	    }

        [HttpGet]
        public async Task<IActionResult> Index(int? page, int? pageSize)
        {
	        return View(await GetPaginatedReview(page, pageSize));
        }

        [HttpPost]
        public IActionResult Accept(long id)
        {
            AcceptReview(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
