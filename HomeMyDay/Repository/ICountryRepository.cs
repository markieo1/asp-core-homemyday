using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Home.Repository
{
    public interface ICountryRepository
    {
		/// <summary>
		/// An IEnumerable of all countries.
		/// </summary>
		IEnumerable<Country> Countries { get; }

		/// <summary>
		/// Find a single country by ID.
		/// </summary>
		/// <param name="id">The ID to search by.</param>
		/// <returns>The country.</returns>
		Country GetCountry(long id);
    }
}
