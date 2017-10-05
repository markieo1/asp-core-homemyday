using HomeMyDay.Database.Identity;
using HomeMyDay.Helpers;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeMyDay.Controllers.Cms
{
    [Area("CMS")]
    [Authorize(Policy = IdentityPolicies.Administrator)]
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
