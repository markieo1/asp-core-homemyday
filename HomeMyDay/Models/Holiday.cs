using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Models
{
    public class Holiday
    {
		/// <summary>
		/// The accommodation where the customer is staying.
		/// </summary>
		public Accommodation Accommodation { get; set; }

		/// <summary>
		/// The departure date of the flight.
		/// </summary>
		public DateTime DepartureDate { get; set; }

		/// <summary>
		/// The departure date of the return flight.
		/// </summary>
		public DateTime ReturnDate { get; set; }

		/// <summary>
		/// The amount of people who are traveling.
		/// </summary>
		public int NrPersons { get; set; }
    }
}
