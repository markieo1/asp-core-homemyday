using System.Threading.Tasks;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Web.Base.ViewModels;

namespace HomeMyDay.Web.Base.Managers.Implementation
{
	public class PageManager : IPageManager
	{
		private readonly IPageRepository _pageRepository;

		public PageManager(IPageRepository pageRepository)
		{
			_pageRepository = pageRepository;
		}	 

		public void EditPage(long id, Page page)
		{
			var suprise = _pageRepository.GetPage(id);
			if (suprise != null)
			{
				_pageRepository.EditPage(id, page);
			}
		}

		public PageViewModel GetPageViewModel(long id)
		{
			var suprise = _pageRepository.GetPage(id);
			return new PageViewModel()
			{
				Title = suprise.Title,
				Content = suprise.Content
			}; 
		}

		public Task<PaginatedList<Page>> GetPagePaginatedList(int? page, int? pageSize)
		{
			return _pageRepository.List(page ?? 1, pageSize ?? 5);
		}
	}
}
