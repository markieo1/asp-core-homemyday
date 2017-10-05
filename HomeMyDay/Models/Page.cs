using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Models
{
    public class Page : BaseModel
    {
		/// <summary>
		/// A unique Id to call for the page
		/// </summary>
		public string Page_Id { get; set; }

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
