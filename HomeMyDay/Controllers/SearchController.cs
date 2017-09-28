using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.ViewModels;
using HomeMyDay.Repository;

namespace HomeMyDay.Controllers
{
	public class SearchController : Controller
	{
		private readonly IAccommodationRepository _accommodationRepo;

		public SearchController(IAccommodationRepository repo)
		{
			this._accommodationRepo = repo;
		}

		[HttpPost]
		public ViewResult Results(AccommodationSearchViewModel search)
		{
			var searchResultsModel = new AccommodationSearchResultsViewModel
			{
				//Store the original search parameters
				Search = search,

				//Perform search
				Accommodations = _accommodationRepo.Search(search.Location, search.StartDate, search.EndDate, search.Persons)
			};

			if (searchResultsModel.Accommodations.Any())
			{
				return View("Results", searchResultsModel);
			}
			else
			{
				return View("NoResults", searchResultsModel);
			}
		}
	}
}