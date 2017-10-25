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
		//private readonly IBookingManager BookingManager;

		//public BookingController(IBookingManager BookingMgr)
		//{
			//BookingyManager = BookingMgr;
		//}

		[HttpGet]
		public IEnumerable<Booking> Get()
		{
			//return BookingManager.GetCountries();

		}

		// GET api/values
		[HttpGet("{id}")]
		public Booking Get(int id)
        {
			//return BookingManager.GetBooking(id);
			return new Booking();
		}

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Booking Booking)
        {
			//BookingManager.Save(Booking);

			return CreatedAtAction(nameof(Get), new { id = Booking.Id }, Booking);
        }

		public IActionResult Put([FromBody]Booking[] bookings)
		{
			return BadRequest();
		}

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Booking booking)
        {
			return BadRequest();
		}

		[HttpDelete]
		public IActionResult Delete()
		{
			return BadRequest();
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
			return BadRequest();
        }
    }
}
