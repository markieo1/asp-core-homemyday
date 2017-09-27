using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Repository
{
	public interface IAccommodationRepository
	{
		/// <summary>
		/// Gets the accommmodations.
		/// </summary>
		/// <returns>IEnumerable containing all accommodations</returns>
		IEnumerable<Accommodation> Accommodations { get; }

		/// <summary>
		/// Gets one accommodation.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException">id</exception>
		/// <exception cref="KeyNotFoundException"></exception>
		Accommodation GetAccommodation(long id);
	}
}
