using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Web.Base.ViewModels;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class SearchController : Controller
	{
		private readonly IAccommodationManager _accommodationManager;

		public SearchController(IAccommodationManager accommodationManager)
		{
			_accommodationManager = accommodationManager;
		}

		[HttpPost]
		public ViewResult Results(AccommodationSearchViewModel search)
		{
			var searchResultsModel = new AccommodationSearchResultsViewModel
			{
				//Store the original search parameters
				Search = search,

				//Perform search
				Accommodations = _accommodationManager.Search(search.Location, search.StartDate ?? DateTime.Now, search.EndDate ?? DateTime.Now.AddDays(1), search.Persons)
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