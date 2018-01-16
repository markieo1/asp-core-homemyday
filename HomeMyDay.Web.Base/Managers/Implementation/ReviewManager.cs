using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Core.Models;
using HomeMyDay.Web.Base.ViewModels;
using HomeMyDay.Core.Repository;

namespace HomeMyDay.Web.Base.Managers.Implementation
{
	public class ReviewManager : IReviewManager
    {
	    private readonly IReviewRepository _reviewRepository;

	    public ReviewManager(IReviewRepository reviewRepository)
	    {
		    _reviewRepository = reviewRepository;
	    }

	    public IEnumerable<ReviewViewModel> GetReviews()
	    {
		    return _reviewRepository.Reviews.Select(ReviewViewModel.FromReview).Where(x => x.Approved);
	    }

	    public bool AddReview(string accommodationId, string title, string name, string text)
	    {
		    return _reviewRepository.AddReview(accommodationId, title, name, text);
	    }

	    public Task<PaginatedList<Review>> GetPaginatedReview(int? page, int? pageSize)
	    {
		    return _reviewRepository.List(page ?? 1, pageSize ?? 5);
	    }

	    public void AcceptReview(long id)
	    {
		    _reviewRepository.AcceptReview(id);
	    }

		public IEnumerable<Review> GetAllReviews()
		{
			return _reviewRepository.Reviews;
		}

		public Review GetReview(long id)
		{
			return _reviewRepository.GetReview(id);
		}

		public Task Save(Review review)
		{
			return _reviewRepository.Save(review);
		}

		public Task Delete(long id)
		{
			return _reviewRepository.Delete(id);
		}
	}
}
