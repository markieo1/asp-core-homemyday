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
			List<Accommodation> accommodations = accommodationManager.GetAccommodations().ToList();

			//Generate a list of HALResponses
			var response = new List<HALResponse>(accommodations.Count);
			foreach(Accommodation accommodation in accommodations)
			{
				response.Add(
					new HALResponse(accommodation)
					.AddLinks(new Link[] {
						new Link(Link.RelForSelf, $"api/v1/accommodations/{accommodation.Id}")
					})
				);
			}

			return this.Ok(response);
		}

		// GET api/values
		[HttpGet("{id}")]
		public Accommodation Get(int id)
        {
			return accommodationManager.GetAccommodation(id);
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

			return CreatedAtAction(nameof(Get), new { id = accommodation.Id }, accommodation);
        }

		[HttpPut]
		public IActionResult Put([FromBody]Accommodation[] accommodations)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (Accommodation accommodation in accommodations)
			{
				accommodationManager.Save(accommodation);
			}

			return Accepted();
		}

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Accommodation accommodation)
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

			return AcceptedAtAction(nameof(Get), new { id = accommodation.Id }, accommodation);
        }

		[HttpDelete]
		public async Task<IActionResult> DeleteAsync()
		{
			IEnumerable<Accommodation> accommodations = accommodationManager.GetAccommodations();
			foreach(Accommodation accommodation in accommodations)
			{
				await accommodationManager.Delete(accommodation.Id);
			}

			return Accepted();
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
			accommodationManager.Delete(id);

			return AcceptedAtAction(nameof(Get));
        }
    }
}
