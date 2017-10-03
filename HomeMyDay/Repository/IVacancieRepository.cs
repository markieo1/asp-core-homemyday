using System;
using HomeMyDay.Models;
using System.Collections.Generic;

namespace HomeMyDay.Repository
{
	public interface IVacancieRepository
    {
        /// <summary>
		/// Gets the Vacancies.
		/// </summary>
		/// <returns>IEnumerable containing all vacancies</returns>
        IEnumerable<Vacancy> Vacancies { get; }

        /// <summary>
        /// Gets one vacancie.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">id</exception>
        /// <exception cref="KeyNotFoundException"></exception>
        Vacancy GetVacancie(long id);
    }
}
