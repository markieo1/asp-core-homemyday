using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Api.Controllers
{
    public class CountriesController : BaseApiController
    {
		private readonly ICountryManager countryManager;

		public CountriesController(ICountryManager countryMgr)
		{
			countryManager = countryMgr;
		}

		[HttpGet]
		public IEnumerable<Country> Get()
		{
			return countryManager.GetCountries();
		}

		// GET api/values
		[HttpGet("{id}")]
		public Country Get(int id)
        {
			return countryManager.GetCountry(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Country Country)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			countryManager.Save(Country);

			return CreatedAtAction(nameof(Get), new { id = Country.Id }, Country);
        }

		[HttpPut]
		public IActionResult Put([FromBody]Country[] countries)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (Country country in countries)
			{
				countryManager.Save(country);
			}

			return Accepted();
		}

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Country country)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (country.Id != id)
			{
				return BadRequest();
			}

			countryManager.Save(country);

			return AcceptedAtAction(nameof(Get), new { id = country.Id }, country);
        }

		[HttpDelete]
		public IActionResult Delete()
		{
			IEnumerable<Country> Countrys = countryManager.GetCountries();
			foreach(Country Country in Countrys)
			{
				countryManager.Delete(Country.Id);
			}

			return Accepted();
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
			countryManager.Delete(id);

			return AcceptedAtAction(nameof(Get));
        }
    }
}
