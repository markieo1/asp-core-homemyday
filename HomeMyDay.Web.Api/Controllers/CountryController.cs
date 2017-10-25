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
    [Route("api/countries")]
    public class CountryController : Controller
    {
		private readonly ICountryManager CountryManager;

		public CountryController(ICountryManager CountryMgr)
		{
			CountryManager = CountryMgr;
		}

		[HttpGet]
		public IEnumerable<Country> Get()
		{
			return CountryManager.GetCountries();
		}

		// GET api/values
		[HttpGet("{id}")]
		public Country Get(int id)
        {
			return CountryManager.GetCountry(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Country Country)
        {
			//CountryManager.(Country);

			return CreatedAtAction(nameof(Get), new { id = Country.Id }, Country);
        }

		[HttpPut]
		public IActionResult Put([FromBody]Country[] countries)
		{
			foreach(Country country in countries)
			{
				CountryManager.Save(country);
			}

			return Accepted();
		}

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Country country)
        {
			if(country.Id != id)
			{
				return BadRequest();
			}

			CountryManager.Save(country);

			return AcceptedAtAction(nameof(Get), new { id = country.Id }, country);
        }

		[HttpDelete]
		public IActionResult Delete()
		{
			IEnumerable<Country> Countrys = CountryManager.GetCountries();
			foreach(Country Country in Countrys)
			{
				CountryManager.Delete(Country.Id);
			}

			return Accepted();
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
			CountryManager.Delete(id);

			return AcceptedAtAction(nameof(Get));
        }
    }
}
