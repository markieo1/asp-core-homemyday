using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Repository
{
	/// <summary>
	/// Repository inteface to load all the holidays
	/// </summary>
	public interface IHolidayRepository
	{
		/// <summary>
		/// Get all holidays from the repository.
		/// </summary>
		/// <returns>IEnumerable containing all Holidays.</returns>
		IEnumerable<Holiday> Holidays { get; }

        IEnumerable<Holiday> GetRecommendedHolidays(IEnumerable<Holiday> holidays);

        /// <summary>
        /// Searches possible holidays for the specified parameters.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="departure">The departure date.</param>
        /// <param name="returnDate">The return date.</param>
        /// <param name="amountOfGuests">The amount of guests.</param>
        /// <returns>IEnumerable containing all search results</returns>
        IEnumerable<Holiday> Search(string location, DateTime departure, DateTime returnDate, int amountOfGuests);

		/// <summary>
		/// Gets the available locations for holidays.
		/// </summary>
		/// <returns>IEnumerable containing all locations for holidays</returns>
		IEnumerable<Accommodation> Accommodations { get; }
	}
}
