using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Core.Extensions
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

		/// <summary>
		/// Truncates the string to the specified amount of characters.
		/// </summary>
		/// <param name="value">The string.</param>
		/// <param name="maxLength">The max length of the string.</param>
		/// <returns></returns>
		public static string Truncate(this string value, int maxLength)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}

			if (value.Length <= maxLength)
			{
				return value;
			}
			else
			{
				return $"{value.Substring(0, maxLength)}...";
			}
		}

		/// <summary>
		/// Replaces the whitespaces in a string with dashes
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string Dashed(this string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return value;
			}

			string trimmedValue = value.Trim();
			return trimmedValue.Replace(" ", "-");
		}
	}
}
