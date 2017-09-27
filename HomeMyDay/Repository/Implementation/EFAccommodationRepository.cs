using HomeMyDay.Database;
using HomeMyDay.Models;
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

			Accommodation accommodation = Accommodations.FirstOrDefault(a => a.Id == id);
			if (accommodation == null)
			{
				throw new KeyNotFoundException($"Accommodation with ID: {id} is not found");
			}

			return accommodation;
		}
	}
}
