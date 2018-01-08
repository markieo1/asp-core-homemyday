using System.Threading.Tasks;
using HomeMyDay.Core.Models;
using HomeMyDay.Web.Base.ViewModels;
using System.Collections.Generic;

namespace HomeMyDay.Web.Base.Managers
{
	public interface IPageManager
    {
	    Task<PaginatedList<Page>> GetPagePaginatedList(int? page, int? pageSize);

	    PageViewModel GetPageViewModel(long id);

		IEnumerable<Page> GetPages();

		Page GetPage(long id);
						
	    Task EditPage(long id, Page page);

		Task DeletePage(long id);

		void AddPage(Page page);
	}
}
