using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Extensions
{
	public static class StringExtensions
	{
		/// <summary>
		/// Trims the name of the controller.
		/// </summary>
		/// <param name="str">The string.</param>
		/// <returns></returns>
		public static string TrimControllerName(this string str)
		{
			return str.Replace("Controller", "");
		}
	}
}
