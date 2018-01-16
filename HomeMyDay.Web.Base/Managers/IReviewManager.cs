using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMyDay.Core.Models;
using HomeMyDay.Web.Base.ViewModels;

namespace HomeMyDay.Web.Base.Managers
{
	public interface IReviewManager
    {
	    IEnumerable<ReviewViewModel> GetReviews();

	    IEnumerable<Review> GetAllReviews();

		bool AddReview(string accommodationId, string title, string name, string text);

	    Task<PaginatedList<Review>> GetPaginatedReview(int? page, int? pageSize);

	    void AcceptReview(long id);

		Review GetReview(long id);

		Task Save(Review review);

		Task Delete(long id);
	}
}
