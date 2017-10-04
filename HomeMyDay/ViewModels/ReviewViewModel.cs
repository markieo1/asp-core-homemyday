using System;
using System.ComponentModel.DataAnnotations;
using HomeMyDay.Models;

namespace HomeMyDay.ViewModels
{
	public class ReviewViewModel
    {
        /// <summary>
        /// The Name of the user who filled in the form
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The Name of the user who filled in the form
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The review text
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// The date of when the review is placed
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

		/// <summary>
		/// The id of the accommodation
		/// </summary>
		public Accommodation Accommodation { get; set; }
    }
}
