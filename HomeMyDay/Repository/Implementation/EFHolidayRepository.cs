using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Repository.Implementation
{
	public class EFHolidayRepository : IHolidayRepository
	{
		private readonly object _context;

		public EFHolidayRepository(object context)
		{
			_context = context;
		}

		public IEnumerable<Holiday> Search(string location, DateTime arrival, DateTime leave, int amountOfGuests)
		{
			if (string.IsNullOrWhiteSpace(location))
			{
				throw new ArgumentNullException(nameof(location));
			}

			if (arrival == default(DateTime))
			{
				throw new ArgumentOutOfRangeException(nameof(arrival));
			}

			if (leave == default(DateTime))
			{
				throw new ArgumentOutOfRangeException(nameof(leave));
			}

			if (amountOfGuests <= 0)
			{
				throw new ArgumentNullException(nameof(amountOfGuests));
			}

			return new List<Holiday>();
		}
	}
}
