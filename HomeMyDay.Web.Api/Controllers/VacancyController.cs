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
    [Route("api/vacancies")]
    public class VacancyController : Controller
    {
        private readonly IVacancyManager vacancyManager;

        public VacancyController(IVacancyManager vacancyMgr)
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
        public Vacancy Get(long id)
        {
            return vacancyManager.GetVacancy(id);
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

            return Accepted();
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

            return AcceptedAtAction(nameof(Get), new { id = vacancy.Id }, vacancy);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            IEnumerable<Vacancy> vacancies = vacancyManager.GetVacancies();

            foreach (Vacancy vacancy in vacancies)
            {
                await vacancyManager.Delete(vacancy.Id);
            }

            return Accepted();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            vacancyManager.Delete(id);

            return AcceptedAtAction(nameof(Get));
        }

    }
}
