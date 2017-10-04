using HomeMyDay.Models;
using System.Collections.Generic;

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
        IEnumerable<Review> GetAccomodationReviews(long id);

		/// <summary>
		/// Adds a review to an accommodation
		/// </summary>
		/// <param name="id">The id of the review</param>
		/// <param name="title">The title of the review</param>
		/// <param name="name">The name of the user</param>
		/// <param name="text">The text of the review</param>
		/// <returns></returns>
	    bool AddReview(long accommodationId, string title, string name, string text);
    }
}
