using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.Web.Base.Home.ViewModels
{
    public class LoginViewModel
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
        /// The return URL.
        /// </summary>
        public string ReturnUrl { get; set; } = "/";
    }
}
