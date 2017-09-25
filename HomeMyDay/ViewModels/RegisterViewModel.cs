using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.ViewModels
{
    public class RegisterViewModel
    {
		/// <summary>
		/// The username of the user.
		/// </summary>
        [Required]        
        public string Username { get; set; }

		/// <summary>
		/// The password of the user.
		/// </summary>
		[Required]
		[DataType(DataType.Password)]
        public string Password { get; set; }

		/// <summary>
		/// The email of the user.
		/// </summary>
		[Required]
        [EmailAddress]
        public string Email { get; set; }

		/// <summary>
		/// The return URL.
		/// </summary>
		public string ReturnUrl { get; set; } = "/home";
    }
}
