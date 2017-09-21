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
		private IHolidayRepository holidayRepo;

		public SearchController(IHolidayRepository repo)
		{
			this.holidayRepo = repo;
		}

		[HttpPost]
		public ViewResult Results(HolidaySearchViewModel search)
		{
			var searchResultsModel = new HolidaySearchResultsViewModel();
			//Store the original search parameters
			searchResultsModel.Search = search;

			//Perform search
			searchResultsModel.Holidays = holidayRepo.Search(search.Location, search.StartDate, search.EndDate, search.Persons);

			return View("Results", searchResultsModel);
		}
	}
}