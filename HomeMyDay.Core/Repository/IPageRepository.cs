using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Core.Repository
{
    public interface IPageRepository
    {
		IEnumerable<Page> Pages { get; }

		/// <summary>
		/// Gets the latest surprise.
		/// </summary>
		Page GetPage(long id);

		/// <summary>
		/// Edit the given page
		/// </summary>
		/// <param name="id">the page identifier.</param>
		/// <param name="page">the page model/the data it need to change</param>\
		Task EditPage(long id, Page page);

		/// <summary>
		/// Delete the given page
		/// </summary>
		/// <param name="id">the page identifier.</param>
		Task DeletePage(long id);

		/// <summary>
		/// Add a new page
		/// </summary>
		/// <param name="page">the page.</param>
		void AddPage(Page page);

		/// <summary>
		/// Lists the Page's for the specific page.
		/// </summary>
		/// <param name="page">The page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns></returns>
		Task<PaginatedList<Page>> List(int page = 1, int pageSize = 10);
	}
}
