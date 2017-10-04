using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.ViewModels
{
    public class SuprisePopUpViewModel
    {
		/// <summary>
		/// The Title of the modal
		/// </summary>
		[Required]
		public string Title { get; set; }

		/// <summary>
		/// The Content of the modal
		/// </summary>
		[Required]
		public string Content { get; set; }
	}
}
