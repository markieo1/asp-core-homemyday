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

		/// <summary>
		/// Gets the reviews of the accommodation that is related to the accommodationid
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        IEnumerable<Review> GetAccomodationReviews(long accommodationId);

		/// <summary>
		/// Adds a review to an accommodation
		/// </summary>
		/// <param name="id">The id of the review</param>
		/// <param name="title">The title of the review</param>
		/// <param name="name">The name of the user</param>
		/// <param name="text">The text of the review</param>
		/// <returns></returns>
	    bool AddReview(Accommodation accommodation, string title, string name, string text);

        /// <summary>
		/// Lists the reviews which not accepted.
		/// </summary>
		/// <param name="page">The page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns></returns>
		Task<PaginatedList<Review>> List(int page = 1, int pageSize = 10);

        /// <summary>
        /// Update the review to accepted based on the review id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void AcceptReview(long id);
    }
}
