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
		public IActionResult Get(int id)
        {

	        var result = countryManager.GetCountry(id); ;

	        if (!ModelState.IsValid)
	        {
		        //return 400
		        return BadRequest(ModelState);
	        }

	        if (result == null)
	        {
		        //return 404
		        return NotFound(id);
	        }
	        //return 200
	        return Ok(result);
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

			return Ok(countries);
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

			return Ok(country);
        }

		[HttpDelete]
		public IActionResult Delete()
		{
			IEnumerable<Country> Countrys = countryManager.GetCountries();
			foreach(Country Country in Countrys)
			{
				countryManager.Delete(Country.Id);
			}

			return NoContent();
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
			countryManager.Delete(id);

	        if (countryManager.GetCountry(id) == null)
	        {
		        return NotFound(id);
	        }

	        if (!ModelState.IsValid)
	        {
		        //return 400
		        return BadRequest(ModelState);
	        }

			return NoContent();
        }
    }
}
