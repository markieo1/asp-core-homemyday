using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Base.Home.ViewModels
{
    public class PageViewModel
    {
		/// <summary>
		/// A unique Id to call for the page
		/// </summary>
		public string Page_Name { get; set; }

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
