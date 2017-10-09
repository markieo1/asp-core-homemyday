using System;
using HomeMyDay.Models;
using System.Collections.Generic;
using HomeMyDay.Helpers;
using System.Threading.Tasks;

namespace HomeMyDay.Repository
{
	public interface IVacancyRepository
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
        Vacancy GetVacancy(long id);

        /// <summary>
        /// Lists the Vacancies for the specific page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        Task<PaginatedList<Vacancy>> List(int page = 1, int pageSize = 10);

        /// <summary>
        /// Delete one vacancie.
        /// </summary>
        /// <param name="vacancyId">The identifier.</param>
        Task DeleteVacancy(long vacancyId);

        /// <summary>
        /// Delete one vacancie.
        /// </summary>
        /// <param name="vacancy">The Vacancy Object.</param>
        Task SaveVacancy(Vacancy vacancy);
    }
}
