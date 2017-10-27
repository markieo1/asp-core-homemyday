using System;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Core;
using HomeMyDay.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Core.Repository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HomeMyDay.Infrastructure.Repository
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

		public bool AddReview(long accommodationId, string title, string name, string text)
		{
			bool isAdded;

			if (accommodationId <= 0)
			{
				throw new ArgumentOutOfRangeException();
			}

			var fetchedAccommodation = _accommodationRepository.GetAccommodation(accommodationId);

			try
			{
				if (fetchedAccommodation == null)
				{
					throw new KeyNotFoundException($"Accommodation with ID: {accommodationId} is not found");
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

        public void AcceptReview(long id)
        {
            Review dbEntry = _context.Reviews.FirstOrDefault(s => s.Id == id);
            if (dbEntry.Id >= 1)
            {
                dbEntry.Approved = true;
            }
            _context.SaveChanges();
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

            IQueryable<Review> reviews = _context.Reviews.Where(a => a.Approved == false).OrderBy(x => x.Id).AsNoTracking();

            return PaginatedList<Review>.CreateAsync(reviews, page, pageSize);
        }

		public async Task Save(Review review)
		{
			if (review == null)
			{
				throw new ArgumentNullException(nameof(review));
			}

			if (review.Id <= 0)
			{
				// We are creating a new one
				// Only need to adjust the id to be 0 and save it in the db.
				_context.Reviews.Add(review);
			}
			else
			{
				// Get the tracked review using the ID
				EntityEntry<Review> entityEntry = _context.Entry(review);
				entityEntry.State = EntityState.Modified;
			}

			await _context.SaveChangesAsync();
		}

		public async Task Delete(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(id));
			}

			Review review = _context.Reviews
				.SingleOrDefault(r => r.Id == id);

			if (review == null)
			{
				throw new ArgumentNullException(nameof(id), $"Review with ID: {id} not found!");
			}

			_context.Reviews.Remove(review);

			await _context.SaveChangesAsync();
		}

		public Review GetReview(long id)
		{
			return _context.Reviews.FirstOrDefault(r=>r.Id == id);
		}
	}
}