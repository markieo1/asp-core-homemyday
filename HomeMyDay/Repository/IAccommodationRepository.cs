﻿using HomeMyDay.Models;
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

		/// <summary>
		/// Get all recommended accommodations from repository
		/// </summary>
		/// <returns></returns>
		IEnumerable<Accommodation> GetRecommendedAccommodations();

		/// <summary>
		/// Searches possible accommodations for the specified parameters.
		/// </summary>
		/// <param name="location">The location.</param>
		/// <param name="departure">The departure date.</param>
		/// <param name="returnDate">The return date.</param>
		/// <param name="amountOfGuests">The amount of guests.</param>
		/// <returns>IEnumerable containing all search results</returns>
		IEnumerable<Accommodation> Search(string location, DateTime departure, DateTime returnDate, int amountOfGuests);
	}
}