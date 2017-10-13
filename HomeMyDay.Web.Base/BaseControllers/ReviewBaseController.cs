using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Web.Base.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Base.BaseControllers
{
	public class ReviewBaseController : Controller
    {
	    private readonly IReviewRepository _reviewRepository;

	    protected ReviewBaseController(IReviewRepository repository)
	    {
		    _reviewRepository = repository;
	    }

	    protected IEnumerable<ReviewViewModel> GetReviews()
	    {
			return _reviewRepository.Reviews.Select(ReviewViewModel.FromReview).Where(x => x.Approved);
		}

	    protected bool AddReview(long accommodationId, string title, string name, string text)
	    {
		    return _reviewRepository.AddReview(accommodationId, title, name, text);
	    }

	    protected Task<PaginatedList<Review>> GetPaginatedReview(int? page, int? pageSize)
	    {
		   return _reviewRepository.List(page ?? 1, pageSize ?? 5);
	    }

	    protected void AcceptReview(long id)
	    {
		    _reviewRepository.AcceptReview(id);
	    }  
    }
}
