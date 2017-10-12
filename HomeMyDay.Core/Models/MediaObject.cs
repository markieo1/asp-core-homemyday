using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Core.Models
{
	public class MediaObject : BaseModel
	{
		/// <summary>
		/// Gets or sets the URL of the media.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets the type of this media object.
		/// </summary>
		public MediaType Type { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="MediaObject"/> is primary for displayal.
		/// </summary>
		public bool Primary { get; set; }
	}
}
