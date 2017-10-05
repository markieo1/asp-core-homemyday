using HomeMyDay.Helpers;
using HomeMyDay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeMyDay.Repository
{
    public interface IReviewRepository
    {
        /// <summary>
        /// Gets the reviews.
        /// </summary>
        /// <returns>IEnumerable containing all reviews</returns>
        IEnumerable<Review> Reviews { get; }

        IEnumerable<Review> GetAccomodationReviews(long id);

        /// <summary>
		/// Lists the reviews which not accepted.
		/// </summary>
		/// <param name="page">The page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns></returns>
		Task<PaginatedList<Review>> List(int page = 1, int pageSize = 10);

        // Update GuestResponse
        void AcceptReview(Review review);
    }
}
