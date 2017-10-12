using HomeMyDay.Core.Models;
using HomeMyDay.Web.Home.Repository;
using HomeMyDay.Web.Home.Services;
using HomeMyDay.Web.Home.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeMyDay.Web.Home.Controllers
{
	public class AccommodationController : Controller
	{
		private readonly IAccommodationRepository _accommodationRepository;
		private readonly IReviewRepository _reviewRepository;
		private readonly GoogleApiServiceOptions _googleOptions;

		public AccommodationController(IAccommodationRepository repository, IReviewRepository repo, IOptions<GoogleApiServiceOptions> googleOpts)
		{
			_accommodationRepository = repository;
			_reviewRepository = repo;
			_googleOptions = googleOpts.Value;
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

			ViewBag.GoogleClientApiKey = _googleOptions.ClientApiKey;

			return View(viewModel);
		}
	}
}
