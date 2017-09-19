using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Repository
{
	/// <summary>
	/// Repository inteface to load all the accommodations
	/// </summary>
	public interface IHolidayRepository
	{
		/// <summary>
		/// Searches holidays for the specified parameters.
		/// </summary>
		/// <param name="location">The location.</param>
		/// <param name="arrival">The arrival date.</param>
		/// <param name="leave">The leave date.</param>
		/// <param name="amountOfGuests">The amount of guests.</param>
		/// <returns>IEnumerable containing all search results</returns>
		IEnumerable<Holiday> Search(string location, DateTime arrival, DateTime leave, int amountOfGuests);
	}
}
