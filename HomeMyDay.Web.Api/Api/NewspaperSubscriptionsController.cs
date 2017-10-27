using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Api.Controllers
{
    public class NewspaperSubscriptionsController : BaseApiController
    {
		private readonly INewspaperManager newspaperManager;

		public NewspaperSubscriptionsController(INewspaperManager newspaperMgr)
		{
			newspaperManager = newspaperMgr;
		}

		[HttpGet]
		public IEnumerable<Newspaper> Get()
		{
			return newspaperManager.GetNewspapers();
		}

		// GET api/values
		[HttpGet("{id}")]
		public Newspaper Get(long id)
        {
			return newspaperManager.GetNewspaper(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Newspaper newspaper)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			newspaperManager.Subscribe(newspaper.Email);

			return CreatedAtAction(nameof(Get), new { id = newspaper.Id }, newspaper);
        }

		[HttpDelete]
		public IActionResult Delete()
		{
			IEnumerable<Newspaper> newspapers = newspaperManager.GetNewspapers();
			foreach(Newspaper newspaper in newspapers)
			{
				newspaperManager.Unsubscribe(newspaper.Email);
			}

			return Accepted();
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public IActionResult Delete(long id)
        {
			var newspaper = newspaperManager.GetNewspaper(id);

			newspaperManager.Unsubscribe(newspaper.Email);

			return AcceptedAtAction(nameof(Get));
        }
    }
}
