using HomeMyDay.Database;
using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Repository.Implementation
{
	public class EFHolidayRepository : IHolidayRepository
	{
		private readonly HolidayDbContext _context;

		public EFHolidayRepository(HolidayDbContext context)
		{
			_context = context;
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

			if (returnDate <= departure)
			{
				throw new ArgumentOutOfRangeException(nameof(returnDate));
			}

			var selectQuery = from holiday in _context.Holidays
							  where holiday.Accommodation.Name == location
							  && holiday.NrPersons == amountOfGuests
							  && (departure.Date >= holiday.DepartureDate && departure.Date <= holiday.ReturnDate)
							  && (returnDate.Date <= holiday.ReturnDate)
							  select holiday;

			return selectQuery;
		}
	}
}
