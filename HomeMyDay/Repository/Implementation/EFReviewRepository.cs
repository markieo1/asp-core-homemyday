using HomeMyDay.Database;
using HomeMyDay.Models;
using System.Collections.Generic;
using System.Linq;

namespace HomeMyDay.Repository.Implementation
{
    public class EFReviewRepository : IReviewRepository
    {
        private readonly HomeMyDayDbContext _context;

        public EFReviewRepository(HomeMyDayDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Review> Reviews => _context.Reviews;

        public IEnumerable<Review> GetAccomodationReviews(long id)
        {
            return _context.Reviews.Where(a => a.AccommodationId == id);
        }        
    }
}