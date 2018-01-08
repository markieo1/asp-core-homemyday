using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeMyDay.Core.Repository
{
	public interface IBookingRepository
	{
		IEnumerable<Booking> Bookings { get; }

		Booking GetBooking(long id);

		Task Save(Booking booking);
	}
}