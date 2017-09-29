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

		public AccommodationController(IAccommodationRepository repository)
		{
			_accommodationRepository = repository;
		}

		[HttpGet]
		public IActionResult Detail(long id)
		{
			Accommodation accommodation = null;

			try
			{
				accommodation = _accommodationRepository.GetAccommodation(id);
			}
			catch (ArgumentOutOfRangeException)
			{
				return BadRequest();
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}

			AccommodationViewModel viewModel = new AccommodationViewModel()
			{
				Accommodation = accommodation
			};

			return View(viewModel);
		}
	}
}
