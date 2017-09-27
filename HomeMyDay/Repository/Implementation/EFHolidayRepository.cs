using HomeMyDay.Database;
using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HomeMyDay.Repository.Implementation
{
	public class EFHolidayRepository : IHolidayRepository
	{
		private readonly HolidayDbContext _context;

		public EFHolidayRepository(HolidayDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Holiday> Holidays => _context.Holidays;

		public IEnumerable<Accommodation> Accommodations => _context.Accommodations;

		public Accommodation GetAccommodation(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(id));
			}

			Accommodation accommodation = Accommodations.FirstOrDefault(a => a.Id == id);
			if (accommodation == null)
			{
				throw new KeyNotFoundException($"Accommodation with ID: {id} is not found");
			}

			return accommodation;
		}

		public IEnumerable<Holiday> GetRecommendedHolidays()
		{
			return Holidays.Where(m => m.Recommended == true);
		}

		public IEnumerable<Holiday> Search(string location, DateTime departure, DateTime returnDate, int amountOfGuests)
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

			var selectQuery = from holiday in _context.Holidays.Include(nameof(Holiday.Accommodation))
							  where holiday.Accommodation.Location == searchLocation
							  && (amountOfGuests <= holiday.Accommodation.MaxPersons)
							  && (holiday.DepartureDate >= departure.Date && holiday.ReturnDate <= returnDate.Date)
							  select holiday;

			return selectQuery;
		}
	}
}
