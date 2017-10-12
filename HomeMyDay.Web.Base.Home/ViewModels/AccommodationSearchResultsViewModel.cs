using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Base.Home.ViewModels
{
	public class AccommodationSearchResultsViewModel
	{
		/// <summary>
		/// The original search parameters that the user entered.
		/// </summary>
		public AccommodationSearchViewModel Search { get; set; }

		/// <summary>
		/// The accommodations that should appear in the search results.
		/// </summary>
		public IEnumerable<Accommodation> Accommodations { get; set; }
	}
}
