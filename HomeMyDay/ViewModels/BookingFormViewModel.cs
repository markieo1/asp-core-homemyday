using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Home.ViewModels
{
    public class BookingFormViewModel
    {
		/// <summary>
		/// The ID of the accommodation that the user wants to book.
		/// </summary>
		public long AccommodationId { get; set; }

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
