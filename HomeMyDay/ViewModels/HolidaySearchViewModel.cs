using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Models;

namespace HomeMyDay.ViewModels
{
    public class HolidaySearchViewModel
    {
		/// <summary>
		/// The holidays that should appear in the search results.
		/// </summary>
		public IEnumerable<Holiday> Holidays { get; set; }

		/// <summary>
		/// The amount of pages that should be presented to the user.
		/// </summary>
		public int Pages { get; set; }

		/// <summary>
		/// The current page that the user is on.
		/// </summary>
		public int CurrentPage { get; set; }
    }
}
