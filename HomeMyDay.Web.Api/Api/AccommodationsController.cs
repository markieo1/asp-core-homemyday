﻿using System;
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
		public IActionResult Get(long id)
        {
			return this.HAL(accommodationManager.GetAccommodation(id), new Link[] {
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

			foreach (Accommodation accommodation in accommodations)
			{
				await accommodationManager.Save(accommodation);
			}

			return AcceptedAtAction(nameof(Get));
		}

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]Accommodation accommodation)
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

			return AcceptedAtAction(nameof(Get), new { id = accommodation.Id }, new HALResponse(accommodation).AddLinks(new Link[] {
				new Link(Link.RelForSelf, $"/api/v1/accommodations/{accommodation.Id}")
			}));
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteAsync()
		{
			IEnumerable<Accommodation> accommodations = accommodationManager.GetAccommodations();
			foreach(Accommodation accommodation in accommodations)
			{
				await accommodationManager.Delete(accommodation.Id);
			}

			return AcceptedAtAction(nameof(Get));
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
			accommodationManager.Delete(id);

			return AcceptedAtAction(nameof(Get));
        }
    }
}
