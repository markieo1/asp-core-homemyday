using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.Core.Models
{
	public class Newspaper : BaseModel
    {
		/// <summary>
		/// The email of the user
		/// </summary>
		[Required]
		[EmailAddress]
		public string Email { get; set; }
    }
}
