using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Web.Base.ViewModels;
using System.Linq;

namespace HomeMyDay.Web.Base.Managers.Implementation
{
	public class FaqManager	: IFaqManager
    {
	    private readonly IFaqRepository _faqRepository;

	    public FaqManager(IFaqRepository repository)
	    {
		    _faqRepository = repository;
	    }

		public IEnumerable<FaqCategory> GetFaqCategories()
	    {
		    return _faqRepository.GetCategoriesAndQuestions();
	    }

		public IEnumerable<FaqCategory> GetFaqQuestions(long id)
		{
			return _faqRepository.GetCategoriesAndQuestions().Where(q=>q.Id == id);
		}

		public Task<PaginatedList<FaqCategory>> GetFaqCategoryPaginatedList(int? page, int? pageSize)
	    {
		    var paginatedResult = _faqRepository.ListCategories(page ?? 1, pageSize ?? 5);
		    return paginatedResult;
	    }

	    public async Task<FaqQuestionsViewModel> GetFaqQuestionsViewModel(long id, int? page, int? pageSize)
	    {
		    return new FaqQuestionsViewModel()
		    {
			    Category = _faqRepository.GetCategory(id),
			    Questions = await _faqRepository.ListQuestions(id, page ?? 1, pageSize ?? 5)
		    };
	    }

	    public FaqCategory GetFaqCategory(long id)
	    {
		    FaqCategory category;

		    if (id <= 0)
		    {
			    category = new FaqCategory();
		    }
		    else
		    {
			    category = _faqRepository.GetCategory(id);
		    }

		    return category;
	    }

	    public Task SaveCategory(FaqCategory category)
	    {
		    return _faqRepository.SaveCategory(category);
	    }

	    public Task DeleteCategory(long id)
	    {
		    return _faqRepository.DeleteCategory(id);
	    }

		IEnumerable<FaqQuestion> IFaqManager.GetFaqQuestions(long id)
		{
			return _faqRepository.GetQuestions(id);
		}

		public FaqQuestion GetFaqQuestion(long id)
		{
			return _faqRepository.GetQuestion(id);
		}

		public Task SaveQuestion(FaqQuestion question)
		{
			return _faqRepository.SaveQuestion(question);
		}

		public Task DeleteQuestion(long id)
		{
			return _faqRepository.DeleteQuestion(id);
		}
	}
}
