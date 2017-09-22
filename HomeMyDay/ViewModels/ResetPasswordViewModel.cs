using System;
using System.ComponentModel;
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
		[DisplayName("Nieuw wachtwoord")]
		[Required(ErrorMessage = "Vul een nieuw wachtwoord in.")]
		public string NewPassword { get; set; }

		/// <summary>
		/// Gets or sets the new password confirmation of the user.
		/// </summary>
		[DisplayName("Herhaal nieuw wachtwoord")]
		[Required(ErrorMessage = "Het wachtwoord komt niet overeen met het nieuwe wachtwoord.")]
		public string NewPasswordConfirmation { get; set; }
    }
}
