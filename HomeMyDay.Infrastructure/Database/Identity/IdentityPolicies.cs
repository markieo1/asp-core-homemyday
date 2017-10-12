using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Home.Database.Identity
{
	/// <summary>
	/// Class that contains all policies
	/// </summary>
	public static class IdentityPolicies
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
