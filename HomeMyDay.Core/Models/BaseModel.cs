using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
