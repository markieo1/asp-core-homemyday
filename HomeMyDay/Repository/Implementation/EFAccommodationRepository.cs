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
	public class EFAccommodationRepository : IAccommodationRepository
	{
		private readonly HomeMyDayDbContext _context;

		public EFAccommodationRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Accommodation> Accommodations => _context.Accommodations;

		public Accommodation GetAccommodation(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(id));
			}

			Accommodation accommodation = _context.Accommodations
				.Include(nameof(Accommodation.MediaObjects))
				.FirstOrDefault(a => a.Id == id);

			if (accommodation == null)
			{
				throw new KeyNotFoundException($"Accommodation with ID: {id} is not found");
			}

			return accommodation;
		}

		public IEnumerable<Accommodation> GetRecommendedAccommodations()
		{
			return _context.Accommodations.Include(nameof(Accommodation.MediaObjects)).Where(m => m.Recommended == true);
		}

		public Task<PaginatedList<Accommodation>> List(int page = 1, int pageSize = 10)
		{
			if (page < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(page));
			}

			if (pageSize < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(pageSize));
			}

			IQueryable<Accommodation> accommodations = _context.Accommodations.OrderBy(x => x.Id).AsNoTracking();

			return PaginatedList<Accommodation>.CreateAsync(accommodations, page, pageSize);
		}

		public IEnumerable<Accommodation> Search(string location, DateTime departure, DateTime returnDate, int amountOfGuests)
		{
			if (string.IsNullOrWhiteSpace(location))
			{
				throw new ArgumentNullException(nameof(location));
			}

			if (departure == default(DateTime))
			{
				throw new ArgumentOutOfRangeException(nameof(departure));
			}

			if (returnDate == default(DateTime))
			{
				throw new ArgumentOutOfRangeException(nameof(returnDate));
			}

			if (amountOfGuests <= 0)
			{
				throw new ArgumentNullException(nameof(amountOfGuests));
			}

			if (returnDate.Date <= departure.Date)
			{
				throw new ArgumentOutOfRangeException(nameof(returnDate));
			}

			string searchLocation = location.Trim();

			var selectQuery = from accommodation in _context.Accommodations
							  where accommodation.Location == searchLocation
							  && (amountOfGuests <= accommodation.MaxPersons
							  && (accommodation.NotAvailableDates.Count == 0 || accommodation.NotAvailableDates.Any(x => (x.Date.Date != departure.Date || x.Date != returnDate.Date))))
							  select accommodation;

			return selectQuery;
		}
	}
}
