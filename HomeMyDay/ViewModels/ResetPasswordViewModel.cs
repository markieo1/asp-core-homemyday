using System;
using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.Web.Home.ViewModels
{
    public class ResetPasswordViewModel
    {
		/// <summary>
		/// Gets or sets the Id of the user.
		/// </summary>
		[Required]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the Email of the user.
		/// </summary>
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the Password of the user.
		/// </summary>
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		/// <summary>
		/// get and set second password and compare Password of the user.
		/// </summary>
		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		/// <summary>
		/// Gets or sets the Confirm Code of the user.
		/// </summary>
		[Required]
		public string Code { get; set; }
	}
}
