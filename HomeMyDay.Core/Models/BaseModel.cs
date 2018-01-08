using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.Core.Models
{
	/// <summary>
	/// Base model class
	/// </summary>
	public class BaseModel
	{
		/// <summary>
		/// The ID of the model.
		/// </summary>
		[Key]
		public long Id { get; set; }   
	}
}
