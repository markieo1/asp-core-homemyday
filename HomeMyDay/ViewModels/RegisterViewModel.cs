using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.ViewModels
{
    public class RegisterViewModel
    {
        [Required]        
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string ReturnUrl { get; set; } = "/home";
    }
}
