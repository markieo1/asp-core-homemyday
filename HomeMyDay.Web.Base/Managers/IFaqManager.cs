using HomeMyDay.Core.Models;
using HomeMyDay.Web.Base.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Base.Managers
{
	public interface IFaqManager
	{
		IEnumerable<FaqCategory> GetFaqCategories();

		IEnumerable<FaqQuestion> GetFaqQuestions(long id);

		Task<PaginatedList<FaqCategory>> GetFaqCategoryPaginatedList(int? page, int? pageSize);

		Task<FaqQuestionsViewModel> GetFaqQuestionsViewModel(long id, int? page, int? pageSize);

		FaqCategory GetFaqCategory(long id);

		Task SaveCategory(FaqCategory category);

		Task DeleteCategory(long id);

		FaqQuestion GetFaqQuestion(long id);

		Task SaveQuestion(FaqQuestion question);

		Task DeleteQuestion(long id);
	}
}
