using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace HomeMyDay.Web.Site.Cms.Extensions
{
	public static class RazorViewEngineOptionsExtensions
	{
		/// <summary>
		/// Adds the CMS views.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public static RazorViewEngineOptions AddCmsViews(this RazorViewEngineOptions options)
		{
			options.FileProviders.Add(new EmbeddedFileProvider(
				typeof(RazorViewEngineOptionsExtensions).GetTypeInfo().Assembly
			));

			options.AreaViewLocationFormats.Add("/Views/{2}/{1}/{0}.cshtml");
			options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");

			return options;
		}
	}
}
