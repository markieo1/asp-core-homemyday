using System;
using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.ViewModels
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
		/// Gets or sets the Passorws of the user.
		/// </summary>
		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		/// <summary>
		/// Gets or sets the Confirm Code of the user.
		/// </summary>
		public string Code { get; set; }
	}
}
