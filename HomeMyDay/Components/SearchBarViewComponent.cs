using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Web.Home.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Home.Components
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
