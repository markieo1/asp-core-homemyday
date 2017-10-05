using HomeMyDay.Database;
using HomeMyDay.Helpers;
using HomeMyDay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public void AcceptReview(Review review)
        {
            Review dbEntry = _context.Reviews.FirstOrDefault(s => s.Id == review.Id);
            if (dbEntry != null)
            {
                dbEntry.Approved = review.Approved;
            }
            _context.SaveChanges();
        }

        public IEnumerable<Review> GetAccomodationReviews(long id)
        {
            return _context.Reviews.Where(a => a.AccommodationId == id);
        }

        public Task<PaginatedList<Review>> List(int page = 1, int pageSize = 10)
        {
            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(page));
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            IQueryable<Review> reviews = _context.Reviews.OrderBy(x => x.Id).AsNoTracking();

            return PaginatedList<Review>.CreateAsync(reviews, page, pageSize);
        }
    }
}