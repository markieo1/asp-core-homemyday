using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Controllers
{
	public class AccommodationController : Controller
	{
		private readonly IAccommodationRepository _accommodationRepository;
		private readonly IReviewRepository _reviewRepository;

		public AccommodationController(IAccommodationRepository repository, IReviewRepository repo)
		{
			_accommodationRepository = repository;
			_reviewRepository = repo;
		}

		[HttpGet]
		public IActionResult Detail(long id)
		{
			Accommodation accommodation = null;
			IEnumerable<Review> reviews = null;


			try
			{
				accommodation = _accommodationRepository.GetAccommodation(id);
				reviews = _reviewRepository.GetAccomodationReviews(id);
			}
			catch (ArgumentOutOfRangeException)
			{
				return BadRequest();
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}

			AccommodationViewModel viewModel = AccommodationViewModel.FromAccommodation(accommodation, reviews.Where(x => x.Approved).ToList());

			return View(viewModel);
		}
	}
}
