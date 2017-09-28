using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Components
{
	public class SearchBarViewComponent : ViewComponent
	{
		private readonly IAccommodationRepository _accommodationRepository;

		public SearchBarViewComponent(IAccommodationRepository repository)
		{
			_accommodationRepository = repository;
		}

		public IViewComponentResult Invoke()
		{
			IEnumerable<Accommodation> accommodations = _accommodationRepository.Accommodations;

			AccommodationSearchViewModel viewModel = new AccommodationSearchViewModel
			{
				Accommodations = accommodations
			};

			return View(viewModel);
		}
	}
}
