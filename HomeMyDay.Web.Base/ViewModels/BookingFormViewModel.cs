using System.Collections.Generic;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Base.ViewModels
{
    public class BookingFormViewModel
    {
		/// <summary>
		/// The ID of the accommodation that the user wants to book.
		/// </summary>
		public string AccommodationId { get; set; }

		/// <summary>
		/// The name of the accommodation that the user wants to book.
		/// </summary>
		public string AccommodationName { get; set; }

		/// <summary>
		/// A list of people who are booking the trip.
		/// </summary>
		public List<BookingPerson> Persons { get; set; }
    }
}
