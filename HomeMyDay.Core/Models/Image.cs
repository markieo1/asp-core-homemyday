using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMyDay.Core.Models
{
    public class Image
    {
		/// <summary>
		/// Gets or sets the uuid
		/// </summary>
		public string Uuid { get; set; }

		/// <summary>
		/// Gets or sets the file name
		/// </summary>
		public string Filename { get; set; }

		/// <summary>
		/// Gets or sets the file size
		/// </summary>
		public int FileSize { get; set; }

		/// <summary>
		/// Gets or sets the title
		/// </summary>
		public string Title { get; set; }
    }
}
