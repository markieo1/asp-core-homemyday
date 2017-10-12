using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Infrastructure.Identity
{
	/// <summary>
	/// This class contains all the roles for the identity framework
	/// </summary>
	public static class IdentityRoles
	{
		/// <summary>
		/// The administrator
		/// </summary>
		public const string Administrator = "Administrator";

		/// <summary>
		/// The booker
		/// </summary>
		public const string Booker = "Booker";
	}
}
