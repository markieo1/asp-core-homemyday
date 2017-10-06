using HomeMyDay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMyDay.Helpers;

namespace HomeMyDay.Repository
{
	public interface IFaqRepository
    {
        /// <summary>
		/// Get All Categories and Questions 
		/// </summary>
		/// <returns>The categories and the linked questions to a categorie</returns>
        IEnumerable<FaqCategory> GetCategoriesAndQuestions();

		/// <summary>
		/// Lists the FaqCategories for the specific page.
		/// </summary>
		/// <param name="page">The page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns></returns>
	    Task<PaginatedList<FaqCategory>> List(int page = 1, int pageSize = 10);
    }
}
