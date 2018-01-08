using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.Core.Models
{
	public class Page : BaseModel
    {
		/// <summary>
		/// The Page Name
		/// </summary>
		public string Page_Name { get; set; }

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
