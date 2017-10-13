using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.ViewModels;

namespace HomeMyDay.Web.Base.BaseControllers
{
	public class FaqBaseController : Controller
    {
	    private readonly IFaqRepository _faqRepository;

	    protected FaqBaseController(IFaqRepository repository)
	    {
		    _faqRepository = repository;
	    }

	    protected IEnumerable<FaqCategory> GetFaqCategories()
	    {
		    return _faqRepository.GetCategoriesAndQuestions();	
	    }

	    protected Task<PaginatedList<FaqCategory>> GetFaqCategoryPaginatedList(int? page, int? pageSize)
	    {		 
			var paginatedResult = _faqRepository.ListCategories(page ?? 1, pageSize ?? 5);
		    return paginatedResult;
	    }

	    protected async Task<FaqQuestionsViewModel> GetFaqQuestionsViewModel(long id, int? page, int? pageSize)
	    {
		    return new FaqQuestionsViewModel()
		    {
			    Category = _faqRepository.GetCategory(id),
			    Questions = await _faqRepository.ListQuestions(id, page ?? 1, pageSize ?? 5)
		    };
	    }

	    protected FaqCategory GetFaqCategory(long id)
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

	    protected Task SaveCategory(FaqCategory category)
	    {
		    return _faqRepository.SaveCategory(category);
	    }

	    protected Task DeleteCategory(long id)
	    {
		    return _faqRepository.DeleteCategory(id);
	    }
    }
}
