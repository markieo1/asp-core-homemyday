using HomeMyDay.Web.Helpers;
using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Web.ViewModels
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
