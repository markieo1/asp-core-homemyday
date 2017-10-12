using HomeMyDay.Core.Authorization;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Controllers.Cms
{
	[Area("CMS")]
    [Authorize(Policy = Policies.Administrator)]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository repository)
        {
            _reviewRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page, int? pageSize)
        {
            PaginatedList<Review> paginatedResult = await _reviewRepository.List(page ?? 1, pageSize ?? 5);
            return View(paginatedResult);
        }

        [HttpPost]
        public IActionResult Accept(long id)
        {
            _reviewRepository.AcceptReview(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
