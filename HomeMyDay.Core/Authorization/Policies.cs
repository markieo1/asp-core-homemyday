using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMyDay.Core.Authorization
{
	/// <summary>
	/// Class that contains all policies
	/// </summary>
	public static class Policies
	{
		/// <summary>
		/// Requires the user to have the policy administrator
		/// </summary>
		public const string Administrator = "RequireAdministrator";

		/// <summary>
		/// Requires the user to have the policy booker
		/// </summary>
		public const string Booker = "RequireBooker";
	}
}
