using HomeMyDay.Database;
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
		private readonly HolidayDbContext _context;

		public EFAccommodationRepository(HolidayDbContext context)
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
			return Accommodations.Where(m => m.Recommended == true);
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
							  && (amountOfGuests <= accommodation.MaxPersons)
							  //&& (accommodation.DepartureDate >= departure.Date && accommodation.ReturnDate <= returnDate.Date)
							  select accommodation;

			return selectQuery;
		}
	}
}
