using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Core;
using HomeMyDay.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Core.Repository;

namespace HomeMyDay.Infrastructure.Repository
{
	public class EFAccommodationRepository : IAccommodationRepository
	{
		private readonly HomeMyDayDbContext _context;

		public EFAccommodationRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Accommodation> Accommodations => _context.Accommodations;

		public async Task Delete(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				throw new ArgumentOutOfRangeException(nameof(id));
			}

			Accommodation accommodation = _context.Accommodations
                .Include(x => x.MediaObjects)
                .Include(x => x.Reviews)
                .Include(x => x.NotAvailableDates)
                .SingleOrDefault(a => a.Id == id);

			if (accommodation == null)
			{
				throw new ArgumentNullException(nameof(id), $"Accommodation with ID: {id} not found!");
			}

			_context.Accommodations.Remove(accommodation);

			await _context.SaveChangesAsync();
		}

		public Accommodation GetAccommodation(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
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
			// Reset to default value
			if (pageSize <= 0)
			{
				pageSize = 10;
			}

			// We are not able to skip before the first page
			if (page <= 0)
			{
				page = 1;
			}

			IQueryable<Accommodation> accommodations = _context.Accommodations.OrderBy(x => x.Id).AsNoTracking();

			return PaginatedList<Accommodation>.CreateAsync(accommodations, page, pageSize);
		}

		public async Task Save(Accommodation accommodation)
		{
			if (accommodation == null)
			{
				throw new ArgumentNullException(nameof(accommodation));
			}

			if (string.IsNullOrWhiteSpace(accommodation.Id))
			{
				// We are creating a new one
				// Only need to adjust the id to be 0 and save it in the db.
				_context.Accommodations.Add(accommodation);
			}
			else
			{
				// Get the tracked accommodation using the ID
				EntityEntry<Accommodation> entityEntry = _context.Entry(accommodation);
				entityEntry.State = EntityState.Modified;
			}

			await _context.SaveChangesAsync();
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
