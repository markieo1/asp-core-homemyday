using HomeMyDay.Helpers;
using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Repository
{
    public interface IPageRepository
    {
		/// <summary>
		/// Gets the latest surprise.
		/// </summary>
		Page GetPage(long id);

		/// <summary>
		/// Edit the given page
		/// </summary>
		/// <param name="pageid">the page identifier.</param>
		/// <param name="page">the page model/the data it need to change</param>\
		void EditPage(long id, Page page);

		Task<PaginatedList<Page>> List(int page = 1, int pageSize = 10);
	}
}
