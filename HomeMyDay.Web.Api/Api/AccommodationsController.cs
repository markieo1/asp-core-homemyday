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
    public class AccommodationsController : BaseApiController
    {
		private readonly IAccommodationManager accommodationManager;

		public AccommodationsController(IAccommodationManager accommodationMgr)
		{
			accommodationManager = accommodationMgr;
		}

		[HttpGet]
		public IActionResult Get()
		{
			IEnumerable<Accommodation> accommodations = accommodationManager.GetAccommodations();

			//Generate a list of HALResponses
			var response = new List<HALResponse>();
			foreach(Accommodation accommodation in accommodations)
			{
				response.Add(
					new HALResponse(accommodation)
					.AddLinks(new Link[] {
						new Link(Link.RelForSelf, $"/api/v1/accommodations/{accommodation.Id}"),
						new Link("updateAccommodation", $"/api/v1/accommodations/{accommodation.Id}", "Update Accommodation", "PUT"),
						new Link("deleteAccommodation", $"/api/v1/accommodations/{accommodation.Id}", "Delete Accommodation", "DELETE")
					})
				);
			}

			return this.Ok(response);
		}

		// GET api/values
		[HttpGet("{id}")]
		public IActionResult Get(string id)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = accommodationManager.GetAccommodation(id);

			if (result == null)
	        {
		        return NotFound(id);
	        }

	        return this.HAL(result, new Link[] {
		        new Link(Link.RelForSelf, $"/api/v1/accommodations/{id}"),
		        new Link("accommodationsList", "/api/v1/accommodations", "Accommodations list"),
		        new Link("updateAccommodation", $"/api/v1/accommodations/{id}", "Update Accommodation", "PUT"),
		        new Link("deleteAccommodation", $"/api/v1/accommodations/{id}", "Delete Accommodation", "DELETE")
	        });
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Accommodation accommodation)
        {
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			accommodationManager.Save(accommodation);

			return CreatedAtAction(nameof(Get), new { id = accommodation.Id }, new HALResponse(accommodation).AddLinks(new Link[] {
				new Link(Link.RelForSelf, $"/api/v1/accommodations/{accommodation.Id}")
			}));
        }

		[HttpPut]
		public async Task<IActionResult> Put([FromBody]Accommodation[] accommodations)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (Accommodation accommodation in accommodations.ToList())
			{
				await accommodationManager.Save(accommodation);
			}

			return Ok(accommodations);
		}

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]Accommodation accommodation)
        {
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (accommodation.Id != id)
			{
				return BadRequest();
			}

			accommodationManager.Save(accommodation);

			return Ok(new HALResponse(accommodation).AddLinks(new Link[] {
				new Link(Link.RelForSelf, $"/api/v1/accommodations/{accommodation.Id}")
			}));
		}

		[HttpDelete]
		public async Task<IActionResult> Delete()
		{
			IEnumerable<Accommodation> accommodations = accommodationManager.GetAccommodations();

			foreach(Accommodation accommodation in accommodations.ToList())
			{
				await accommodationManager.Delete(accommodation.Id);
			}

			return NoContent();
		}

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (accommodationManager.GetAccommodation(id) == null)
	        {
		        return NotFound(id);
	        }

			accommodationManager.Delete(id);

			return NoContent();
        }
    }
}
