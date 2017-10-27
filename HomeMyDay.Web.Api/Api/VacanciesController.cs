using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Api.Controllers
{
    public class VacanciesController : BaseApiController
	{
        private readonly IVacancyManager vacancyManager;

        public VacanciesController(IVacancyManager vacancyMgr)
        {
            vacancyManager = vacancyMgr;
        }

        // EGT api/values
        [HttpGet]
        public IEnumerable<Vacancy> Get()
        {
            return vacancyManager.GetVacancies();
        }

        // GET api/values
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {

			var result = vacancyManager.GetVacancy(id);

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
        public IActionResult Post([FromBody]Vacancy vacancy)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			vacancyManager.Save(vacancy);

            return CreatedAtAction(nameof(Get), new { id = vacancy.Id }, vacancy);
        }

        [HttpPut]
        public IActionResult Put([FromBody]Vacancy[] vacancies)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (Vacancy vacancy in vacancies)
            {
                vacancyManager.Save(vacancy);
            }

            return Ok(vacancies);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]Vacancy vacancy)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (vacancy.Id != id)
            {
                return BadRequest();
            }

            vacancyManager.Save(vacancy);

	        return Ok(vacancy);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            IEnumerable<Vacancy> vacancies = vacancyManager.GetVacancies();

            foreach (Vacancy vacancy in vacancies)
            {
                await vacancyManager.Delete(vacancy.Id);
            }

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            vacancyManager.Delete(id);

	        if (vacancyManager.GetVacancy(id) == null)
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
