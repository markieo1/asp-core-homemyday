using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.ViewModels
{
	public class ForgotPasswordViewModel
	{
		/// <summary>
		/// The email of the user
		/// </summary>
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
