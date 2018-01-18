using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.Core.Models
{
	public class FaqQuestion : BaseModel
	{
		/// <summary>
		/// The Question string of the FAQ
		/// </summary>
		[Display(Name = "Vraag")]
		public string Question { get; set; }

		/// <summary>
		/// The Answer string of a question in the FAQ
		/// </summary>
		[Display(Name = "Antwoord")]
		public string Answer { get; set; }

		/// <summary>
		/// The forgein key constraint on the FaqCategory object
		/// </summary>
		public FaqCategory Category { get; set; }
	}
}