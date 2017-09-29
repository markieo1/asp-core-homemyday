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
    }
}
