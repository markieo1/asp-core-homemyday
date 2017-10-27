using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Models;
using Halcyon.HAL;
using Halcyon.Web.HAL;

namespace HomeMyDay.Web.Api.Controllers
{
	public class BookingsController : BaseApiController
	{
		private readonly IBookingManager bookingManager;

		public BookingsController(IBookingManager bookingMgr)
		{
			bookingManager = bookingMgr;
		}

		[HttpGet]
		public IActionResult Get()
		{
			IEnumerable<Booking> bookings = bookingManager.GetBookings();

			//Generate a list of HALResponses
			var response = new List<HALResponse>();
			foreach (Booking booking in bookings.ToList())
			{
				response.Add(
					new HALResponse(booking)
					.AddLinks(new Link[] {
						new Link(Link.RelForSelf, $"/api/v1/bookings/{booking.Id}")
					})
				);
			}

			return this.Ok(response);
		}

		// GET api/values
		[HttpGet("{id}")]
		public IActionResult Get(long id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = bookingManager.GetBooking(id);

			if (result == null)
			{
				return NotFound(id);
			}

			return Ok(this.HAL(result, new Link[] {
				new Link(Link.RelForSelf, $"/api/v1/bookings/{id}"),
				new Link("bookingsList", "/api/v1/bookings", "Bookings list"),
			}));
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

			return CreatedAtAction(nameof(Get), new { id = booking.Id }, new HALResponse(booking).AddLinks(new Link[] {
				new Link(Link.RelForSelf, $"/api/v1/bookings/{booking.Id}")
			}));
		}

		public IActionResult Put([FromBody]Booking[] bookings)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (Booking booking in bookings.ToList())
			{
				bookingManager.Save(booking);
			}

			return Ok(bookings);
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

			return Ok(new HALResponse(booking).AddLinks(new Link[] {
				new Link(Link.RelForSelf, $"/api/v1/bookings/{booking.Id}")
			}));
		}
	}
}
