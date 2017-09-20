using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.ViewModels
{
    public class HolidaySearchViewModel
    {
		/// <summary>
		/// The location to search for.
		/// </summary>
		public string Location { get; set; }

		/// <summary>
		/// The starting date of the search range.
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// The end date of the search range.
		/// </summary>
		public DateTime EndDate { get; set; }

		/// <summary>
		/// The amount of people that the Accommodation should support.
		/// </summary>
		public int Persons { get; set; }
    }
}
