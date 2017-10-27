using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Models;

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
		public IEnumerable<Accommodation> Get()
		{
			return accommodationManager.GetAccommodations();
		}

		// GET api/values
		[HttpGet("{id}")]
		public IActionResult Get(int id)
        {
			var result = accommodationManager.GetAccommodation(id);

			//check if id is a integer
			//var isNum = int.TryParse(id.ToString(), out var n);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (result == null)
	        {
		        return NotFound(id);
	        }

	        return Ok(result);
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

			return Ok(accommodations);
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

			return Ok(accommodation);
        }

		[HttpDelete]
		public async Task<IActionResult> Delete()
		{
			IEnumerable<Accommodation> accommodations = accommodationManager.GetAccommodations();
			foreach(Accommodation accommodation in accommodations)
			{
				await accommodationManager.Delete(accommodation.Id);
			}

			return NoContent();
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
			accommodationManager.Delete(id);

	        if (accommodationManager.GetAccommodation(id) == null)
	        {
		        return NotFound(id);
	        }

	        if (!ModelState.IsValid)
	        {
		        return BadRequest(ModelState);
	        }

			return NoContent();
        }
    }
}
