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
	public class EFBookingRepository : IBookingRepository
	{
		private readonly HomeMyDayDbContext _context;

		public EFBookingRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Booking> Bookings => _context.Bookings;

		public Booking GetBooking(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(id));
			}

			Booking booking = _context.Bookings
				.Include(nameof(Booking.Persons))
				.FirstOrDefault(a => a.Id == id);

			if (booking == null)
			{
				throw new KeyNotFoundException($"Booking with ID: {id} is not found");
			}

			return booking;
		}

		public async Task Save(Booking booking)
		{
			if (booking == null)
			{
				throw new ArgumentNullException(nameof(booking));
			}

			if (booking.Id <= 0)
			{
				// We are creating a new one
				// Only need to adjust the id to be 0 and save it in the db.
				_context.Bookings.Add(booking);
			}
			else
			{
				// Get the tracked accommodation using the ID
				EntityEntry<Booking> entityEntry = _context.Entry(booking);
				entityEntry.State = EntityState.Modified;
			}

			await _context.SaveChangesAsync();
		}
	}
}
