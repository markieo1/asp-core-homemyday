using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMyDay.Web.Base.ViewModels;

namespace HomeMyDay.Web.Base.Managers
{
	public interface IBookingManager
	{
		IEnumerable<Booking> GetBookings();

		Booking GetBooking(long id);

		Task Save(Booking booking);
	}
}
