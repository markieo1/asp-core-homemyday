using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMyDay.Web.Base.ViewModels
{
	public class FaqQuestionEditViewModel
	{
		/// <summary>
		/// Gets or sets the category identifier.
		/// </summary>
		/// <value>
		/// The category identifier.
		/// </value>
		public long Id { get; set; }

		/// <summary>
		/// Gets or sets the category.
		/// </summary>
		/// <value>
		/// The category.
		/// </value>
		public FaqCategory Category { get; set; }

		/// <summary>
		/// Gets or sets the question identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public long QuestionId { get; set; }

		/// <summary>
		/// Gets or sets the question.
		/// </summary>
		/// <value>
		/// The question.
		/// </value>
		public string Question { get; set; }

		/// <summary>
		/// Gets or sets the answer.
		/// </summary>
		/// <value>
		/// The answer.
		/// </value>
		public string Answer { get; set; }
	}
}
