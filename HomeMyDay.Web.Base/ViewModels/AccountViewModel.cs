using System;

namespace HomeMyDay.Web.Base.ViewModels
{
    public class AccountViewModel
    {
		/// <summary>
		/// The Id of the user.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// The name of the user.
		/// </summary>
		public string Username { get; set; }
    }
}
