using System;
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
            return _context.Reviews.Where(a => a.Accommodation.Id == id);
        }

	    public bool AddReview(Accommodation accommodation, string title, string name, string text)
	    {
		    bool isAdded;
		    try											 
		    {
			    var reviewToAdd = new Review()
			    {
					Accommodation = accommodation,
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
				// log exception
			    isAdded = false;
		    }
		    return isAdded;
	    }
    }
}