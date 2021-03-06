﻿using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HomeMyDay.Web.Site.Home.Extensions
{
	public static class RazorViewEngineOptionsExtensions
	{
		/// <summary>
		/// Adds the home views.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public static RazorViewEngineOptions AddHomeViews(this RazorViewEngineOptions options)
		{
			options.FileProviders.Add(new EmbeddedFileProvider(
				typeof(RazorViewEngineOptionsExtensions).GetTypeInfo().Assembly
			));

			return options;
		}
	}
}
