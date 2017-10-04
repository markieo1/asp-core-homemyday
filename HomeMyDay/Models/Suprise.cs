using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Models
{
    public class Suprise : BaseModel
    {
		/// <summary>
		/// The Title of the modal
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The Content of the modal
		/// </summary>
		public string Content { get; set; }
	}
}
