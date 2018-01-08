using System.ComponentModel.DataAnnotations.Schema;

namespace HomeMyDay.Core.Models
{
	public class FaqQuestion : BaseModel
	{
		/// <summary>
		/// The Question string of the FAQ
		/// </summary>
		public string Question { get; set; }

		/// <summary>
		/// The Answer string of a question in the FAQ
		/// </summary>
		public string Answer { get; set; }

		/// <summary>
		/// The forgein key constraint on the FaqCategory object
		/// </summary>
		public FaqCategory Category { get; set; }
	}
}