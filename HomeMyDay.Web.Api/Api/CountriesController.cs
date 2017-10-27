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
	public class CountriesController : BaseApiController
	{
		private readonly ICountryManager countryManager;

		public CountriesController(ICountryManager countryMgr)
		{
			countryManager = countryMgr;
		}

		[HttpGet]
		public IActionResult Get()
		{
			IEnumerable<Country> countries = countryManager.GetCountries();

			//Generate a list of HALResponses
			var response = new List<HALResponse>();
			foreach (Country country in countries.ToList())
			{
				response.Add(
					new HALResponse(country)
					.AddLinks(new Link[] {
						new Link(Link.RelForSelf, $"/api/v1/countries/{country.Id}"),
						new Link("updateCountry", $"/api/v1/countries/{country.Id}", "Update Country", "PUT"),
						new Link("deleteCountry", $"/api/v1/countries/{country.Id}", "Delete Country", "DELETE")
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
				//return 400
				return BadRequest(ModelState);
			}

			var result = countryManager.GetCountry(id);

			if (result == null)
			{
				//return 404
				return NotFound(id);
			}

			//return 200
			return Ok(this.HAL(result, new Link[] {
				new Link(Link.RelForSelf, $"/api/v1/countries/{id}"),
				new Link("updateCountry", $"/api/v1/countries/{id}", "Update Country", "PUT"),
				new Link("deleteCountry", $"/api/v1/countries/{id}", "Delete Country", "DELETE")
			}));
		}

		// POST api/values
		[HttpPost]
		public IActionResult Post([FromBody]Country country)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			countryManager.Save(country);

			return CreatedAtAction(nameof(Get), new { id = country.Id }, new HALResponse(country).AddLinks(new Link[] {
				new Link(Link.RelForSelf, $"/api/v1/countries/{country.Id}"),
			}));
		}

		[HttpPut]
		public IActionResult Put([FromBody]Country[] countries)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (Country country in countries.ToList())
			{
				countryManager.Save(country);
			}

			return Ok(countries);
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public IActionResult Put(long id, [FromBody]Country country)
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

			return Ok(new HALResponse(country).AddLinks(new Link[] {
				new Link(Link.RelForSelf, $"/api/v1/countries/{country.Id}")
			}));
		}

		[HttpDelete]
		public IActionResult Delete()
		{
			IEnumerable<Country> countrys = countryManager.GetCountries();

			foreach (Country country in countrys.ToList())
			{
				countryManager.Delete(country.Id);
			}

			return NoContent();
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public IActionResult Delete(long id)
		{
			if (!ModelState.IsValid)
			{
				//return 400
				return BadRequest(ModelState);
			}

			if (countryManager.GetCountry(id) == null)
			{
				return NotFound(id);
			}

			countryManager.Delete(id);

			return NoContent();
		}
	}
}
