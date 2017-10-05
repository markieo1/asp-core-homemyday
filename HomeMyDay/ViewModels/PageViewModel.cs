using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.ViewModels
{
    public class PageViewModel
    {
		/// <summary>
		/// A unique Id to call for the page
		/// </summary>
		[BindNever]
		public string Page_Id { get; set; }

		/// <summary>
		/// The Title of the modal
		/// </summary>
		[Required]
		public string Title { get; set; }

		/// <summary>
		/// The Content of the modal
		/// </summary>
		[Required]
		[DataType(DataType.MultilineText)]
		public string Content { get; set; }
	}
}
