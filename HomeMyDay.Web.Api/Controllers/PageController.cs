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
    [Route("api/pages")]
    public class PageController : Controller
    {
		private readonly IPageManager PageManager;

		public PageController(IPageManager PageMgr)
		{
			PageManager = PageMgr;
		}

		[HttpGet]
		public IEnumerable<Page> Get()
		{
			return PageManager.GetPages();
		}

		// GET api/values
		[HttpGet("{id}")]
		public Page Get(int id)
        {
			return PageManager.GetPage(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Page Page)
        {
			PageManager.AddPage(Page);

			return CreatedAtAction(nameof(Get), new { id = Page.Id }, Page);
        }

		[HttpPut]
		public IActionResult Put([FromBody]Page[] pages)
		{
			foreach(Page page in pages)
			{
				PageManager.EditPage(page.Id, page);
			}

			return Accepted();
		}

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Page page)
        {
			if(page.Id != id)
			{
				return BadRequest();
			}

			PageManager.EditPage(id, page);

			return AcceptedAtAction(nameof(Get), new { id = page.Id }, page);
        }

		[HttpDelete]
		public IActionResult Delete()
		{
			IEnumerable<Page> Pages = PageManager.GetPages();
			foreach(Page Page in Pages)
			{
				PageManager.DeletePage(Page.Id);
			}

			return Accepted();
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
			PageManager.DeletePage(id);

			return AcceptedAtAction(nameof(Get));
        }
    }
}
