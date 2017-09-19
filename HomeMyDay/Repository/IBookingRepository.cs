using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Repository
{
	/// <summary>
	/// Repository inteface to load all the bookings
	/// </summary>
	public interface IBookingRepository
	{
		/// <summary>
		/// Searches possible bookings for the specified parameters.
		/// </summary>
		/// <param name="location">The location.</param>
		/// <param name="departure">The departure date.</param>
		/// <param name="returnDate">The return date.</param>
		/// <param name="amountOfGuests">The amount of guests.</param>
		/// <returns>IEnumerable containing all search results</returns>
		IEnumerable<Booking> Search(string location, DateTime departure, DateTime returnDate, int amountOfGuests);
	}
}
