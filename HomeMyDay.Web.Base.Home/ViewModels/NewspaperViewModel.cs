using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.Web.Base.Home.ViewModels
{
	public class NewspaperViewModel
    {
		/// <summary>
		/// The email of the user
		/// </summary>
		[Required]
		[EmailAddress]
		public string Email { get; set; }	
    }
}
