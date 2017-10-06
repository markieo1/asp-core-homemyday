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
		/// Get All Categories
		/// </summary>
		/// <returns>all categories</returns>
		IEnumerable<FaqCategory> Categories { get; }

		/// <summary>
		/// Gets one category.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException">id</exception>
		/// <exception cref="KeyNotFoundException"></exception>
		FaqCategory GetCategory(long id);

		/// <summary>
		/// Delete a Categorie
		/// </summary>
		/// <param name="id">the identifier for which category to delete</param>
		/// <returns></returns>
		Task DeleteCategory(long id);

		/// <summary>
		/// Edit or add a Categorie
		/// </summary>
		/// <param name="category">the object model given to the methode</param>
		/// <returns></returns>
		Task SaveCategory(FaqCategory category);

		/// <summary>
		/// Lists the FaqCategories for the specific page.
		/// </summary>
		/// <param name="page">The page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns></returns>
		Task<PaginatedList<FaqCategory>> List(int page = 1, int pageSize = 10);
    }
}
