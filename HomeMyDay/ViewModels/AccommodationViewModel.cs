using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.ViewModels
{
	public class AccommodationViewModel
	{
		/// <summary>
		/// Gets or sets the accommodation.
		/// </summary>
		/// <value>
		/// The accommodation.
		/// </value>
		public Accommodation Accommodation { get; set; }

		/// <summary>
		/// Gets or sets the detail blocks.
		/// </summary>
		/// <value>
		/// The detail blocks.
		/// </value>
		public IEnumerable<AccommodationDetailBlockViewModel> DetailBlocks { get; set; }
	}
}
