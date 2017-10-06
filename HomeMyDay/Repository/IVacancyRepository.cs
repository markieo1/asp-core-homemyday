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
        Vacancy DeleteVacancy(long vacancyId);

        /// <summary>
        /// Delete one vacancie.
        /// </summary>
        /// <param name="JobTitle">The title.</param>
        /// <param name="Company">The companyName.</param>
        /// <param name="City">The city.</param>
        /// <param name="AboutVacancy">The about text of a vacancy.</param>
        /// <param name="AboutFunction">The about function text of an vacancy.</param>
        /// <param name="JobRequirements">The about job requirements text of a vacancy</param>
        /// <param name="WeOffer">The we offer text of an vacancy.</param>
        void SaveVacancy(string JobTitle, string Company, string City, string AboutVacancy, string AboutFunction, string JobRequirements, string WeOffer);

        /// <summary>
        /// Update a vacancy Record
        /// </summary>
        /// <param name="vacancy">The object identifier.</param>
        void UpdateVacancy(Vacancy vacancy);
    }
}
