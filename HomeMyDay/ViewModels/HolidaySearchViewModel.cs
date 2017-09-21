using HomeMyDay.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.ViewModels
{
	public class HolidaySearchViewModel
	{
		/// <summary>
		/// The accommodations that are available
		/// </summary>
		[BindNever]
		public IEnumerable<Accommodation> Accommodations { get; set; }

		/// <summary>
		/// Gets or sets the selected accommodation.
		/// </summary>
		/// <value>
		/// The accommodation.
		/// </value>
		[Required]
		public Accommodation Accommodation { get; set; }

		/// <summary>
		/// Gets or sets the departure date.
		/// </summary>
		/// <value>
		/// The departure date.
		/// </value>
		[Required]
		public DateTime DepartureDate { get; set; }

		/// <summary>
		/// Gets or sets the return date.
		/// </summary>
		/// <value>
		/// The return date.
		/// </value>
		[Required]
		public DateTime ReturnDate { get; set; }

		/// <summary>
		/// Gets or sets the amount of guests.
		/// </summary>
		/// <value>
		/// The amount of guests.
		/// </value>
		[Required]
		public int AmountOfGuests { get; set; }
	}
}
