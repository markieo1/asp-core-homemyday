using System;
using HomeMyDay.Database;
using HomeMyDay.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
	}
}