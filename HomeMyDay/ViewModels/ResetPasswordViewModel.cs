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
		/// Gets or sets the new password of the user.
		/// </summary>
		[Required]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		/// <summary>
		/// Gets or sets the new password confirmation of the user.
		/// </summary>
		[Required]
		[DataType(DataType.Password)]
		public string NewPasswordConfirmation { get; set; }
    }
}
