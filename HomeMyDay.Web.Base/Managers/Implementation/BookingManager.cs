using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Web.Base.ViewModels;

namespace HomeMyDay.Web.Base.Managers.Implementation
{
	public class BookingManager : IBookingManager
    {
	    private readonly IBookingRepository _bookingRepository;

	    public BookingManager(IBookingRepository bookingRepo)
	    {
		    _bookingRepository = bookingRepo;	
	    }

	    public IEnumerable<Booking> GetBookings()
	    {
		    return _bookingRepository.Bookings;
	    }

	    public Booking GetBooking(long id)
	    {
		    if (id <= 0)
		    {
				return new Booking();
		    }

		    return _bookingRepository.GetBooking(id);
	    }													

	    public Task Save(Booking booking)
	    {
		    return _bookingRepository.Save(booking);
	    }
    }
}
