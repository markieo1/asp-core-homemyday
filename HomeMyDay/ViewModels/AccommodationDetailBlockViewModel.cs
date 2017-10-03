using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.ViewModels
{
	public class AccommodationDetailBlockViewModel
	{
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the icon to display before the title.
		/// </summary>
		/// <value>
		/// The icon.
		/// </value>
		public string Icon { get; set; }

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public string Text { get; set; }

		/// <summary>
		/// Gets a value indicating whether this instance can display.
		/// This is based on the title and text being not whitespace or null
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can display; otherwise, <c>false</c>.
		/// </value>
		public bool CanDisplay
		{
			get
			{
				return !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Text);
			}
		}

        public IEnumerable<Review> Reviews { get; set; }
	}
}
