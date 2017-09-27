using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.ViewModels
{
    public class BookingFormViewModel
    {
		/// <summary>
		/// The accommodation that the user wants to book.
		/// </summary>
		public Accommodation Accommodation { get; set; }
    }
}
