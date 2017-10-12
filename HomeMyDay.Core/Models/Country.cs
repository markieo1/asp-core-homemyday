using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Core.Models
{
    public class Country : BaseModel
    {
		/// <summary>
		/// The country code of the country. (USA, etc.)
		/// </summary>
		public string CountryCode { get; set; }

		/// <summary>
		/// The name of this country.
		/// </summary>
		public string Name { get; set; }
    }
}
