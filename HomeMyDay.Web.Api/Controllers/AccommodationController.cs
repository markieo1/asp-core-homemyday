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
    [Route("api/accommodations")]
    public class AccommodationController : Controller
    {
		private readonly IAccommodationManager accommodationManager;

		public AccommodationController(IAccommodationManager accommodationMgr)
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
		public Accommodation Get(int id)
        {
			return accommodationManager.GetAccommodation(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Accommodation accommodation)
        {
			accommodationManager.Save(accommodation);

			return CreatedAtAction(nameof(Get), new { id = accommodation.Id }, accommodation);
        }

		[HttpPut]
		public IActionResult Put([FromBody]Accommodation[] accommodations)
		{
			foreach(Accommodation accommodation in accommodations)
			{
				accommodationManager.Save(accommodation);
			}

			return Accepted();
		}

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Accommodation accommodation)
        {
			if(accommodation.Id != id)
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
