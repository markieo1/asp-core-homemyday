using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Models
{
	public class MediaObject
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Key]
		public long Id { get; set; }

		/// <summary>
		/// Gets or sets the URL of the media.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets the type of this media object.
		/// </summary>
		public MediaType Type { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string Description { get; set; }
	}
}
