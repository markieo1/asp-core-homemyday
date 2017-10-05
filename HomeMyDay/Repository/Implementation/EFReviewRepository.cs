using System;
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
	    private readonly IAccommodationRepository _accommodationRepository;

        public EFReviewRepository(HomeMyDayDbContext context, IAccommodationRepository accommodationRepository)
        {
            _context = context;
	        _accommodationRepository = accommodationRepository;
        }

        public IEnumerable<Review> Reviews => _context.Reviews;

        public IEnumerable<Review> GetAccomodationReviews(long accommodationId)
        {
	        if (accommodationId <= 0)
	        {
		        throw new ArgumentOutOfRangeException();
	        }

            return _context.Reviews.Where(a => a.Accommodation.Id == accommodationId);
        }

	    public bool AddReview(Accommodation accommodation, string title, string name, string text)
	    {
		    bool isAdded;

		    if (accommodation == null)
		    {
			    throw new ArgumentNullException();
		    }

		    if (accommodation.Id <= 0)
		    {
			    throw new ArgumentOutOfRangeException();
		    }

		    var fetchedAccommodation = _accommodationRepository.GetAccommodation(accommodation.Id);

			try
		    {	
			    if (fetchedAccommodation == null)
			    {
				    throw new KeyNotFoundException();
			    }

				var reviewToAdd = new Review()
			    {
					Accommodation = fetchedAccommodation,
					Title = title,
					Name = name,
					Text = text,
					Date = DateTime.Now
			    };
			    _context.Reviews.Add(reviewToAdd);
			    _context.SaveChanges();	 
			    isAdded = true;
		    }
		    catch (Exception)
		    {					
			    isAdded = false;
		    }
		    return isAdded;
	    }
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