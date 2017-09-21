using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Models;

namespace HomeMyDay.ViewModels
{
    public class HolidaySearchResultsViewModel
    {
		/// <summary>
		/// The original search parameters that the user entered.
		/// </summary>
		public HolidaySearchViewModel Search { get; set; }

		/// <summary>
		/// The holidays that should appear in the search results.
		/// </summary>
		public IEnumerable<Holiday> Holidays { get; set; }
    }
}
