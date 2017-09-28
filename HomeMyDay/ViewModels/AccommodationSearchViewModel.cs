using HomeMyDay.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.ViewModels
{
	public class AccommodationSearchViewModel
	{
		/// <summary>
		/// Gets or sets the available accommodations.
		/// </summary>
		/// <value>
		/// The accommodations.
		/// </value>
		[BindNever]
		public IEnumerable<Accommodation> Accommodations { get; set; }

		/// <summary>
		/// The location to search for.
		/// </summary>
		[Required]
		public string Location { get; set; }

		/// <summary>
		/// The starting date of the search range.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "dd/MM/yyyy")]
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// The end date of the search range.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "dd/MM/yyyy")]
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// The amount of people that the Accommodation should support.
		/// </summary>
		[Required]
		public int Persons { get; set; }
	}
}
