using HomeMyDay.Helpers;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Controllers.Cms
{
    [Area("CMS")]
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


        [HttpPost]
        public async Task<IActionResult> Accept()
        {

        }
    }
}
