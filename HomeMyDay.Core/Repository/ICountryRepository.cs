using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Core.Repository
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

		/// <summary>
		/// Insert or update a country.
		/// </summary>
		/// <param name="country">The country to insert or update. If Id is 0, inserts. Otherwise, updates.</param>
		/// <returns></returns>
		Task Save(Country country);

		/// <summary>
		/// Deletes a country by ID.
		/// </summary>
		/// <param name="id">The ID of the country to delete.</param>
		/// <returns></returns>
		Task Delete(long id);
    }
}
