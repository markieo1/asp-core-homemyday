using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Base.ViewModels
{
	public class FaqQuestionsViewModel
	{
		/// <summary>
		/// Gets or sets the category.
		/// </summary>
		public FaqCategory Category { get; set; }

		/// <summary>
		/// Gets or sets the questions.
		/// </summary>
		public PaginatedList<FaqQuestion> Questions { get; set; }
	}
}
