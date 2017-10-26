using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Api.Controllers
{
	[Produces("application/json")]
    [Route("api/bookings")]
    public class BookingController : Controller
    {
		private readonly IBookingManager bookingManager;

		public BookingController(IBookingManager bookingMgr)
		{
			bookingManager = bookingMgr;
		}

		[HttpGet]
		public IEnumerable<Booking> Get()
		{
			return bookingManager.GetBookings();
		}

		// GET api/values
		[HttpGet("{id}")]
		public Booking Get(long id)
        {
			return bookingManager.GetBooking(id);
		}

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Booking booking)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			bookingManager.Save(booking);

			return CreatedAtAction(nameof(Get), new { id = booking.Id }, booking);
        }

		public async Task<IActionResult> Put([FromBody]Booking[] bookings)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (Booking booking in bookings)
			{
				await bookingManager.Save(booking);
			}

			return Accepted();
		}

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]Booking booking)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			booking.Id = id;
			bookingManager.Save(booking);

			return AcceptedAtAction(nameof(Get), new { id = booking.Id }, booking);
		}
    }
}
