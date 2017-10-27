using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Api.Controllers
{
    public class PagesController : BaseApiController
    {
		private readonly IPageManager pageManager;

		public PagesController(IPageManager pageMgr)
		{
			pageManager = pageMgr;
		}

		[HttpGet]
		public IEnumerable<Page> Get()
		{
			return pageManager.GetPages();
		}

		// GET api/values
		[HttpGet("{id}")]
		public Page Get(long id)
        {
			return pageManager.GetPage(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Page Page)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			pageManager.AddPage(Page);

			return CreatedAtAction(nameof(Get), new { id = Page.Id }, Page);
        }

		[HttpPut]
		public IActionResult Put([FromBody]Page[] pages)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (Page page in pages)
			{
				pageManager.EditPage(page.Id, page);
			}

			return Accepted();
		}

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]Page page)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (page.Id != id)
			{
				return BadRequest();
			}

			pageManager.EditPage(id, page);

			return AcceptedAtAction(nameof(Get), new { id = page.Id }, page);
        }

		[HttpDelete]
		public IActionResult Delete()
		{
			IEnumerable<Page> Pages = pageManager.GetPages();
			foreach(Page Page in Pages)
			{
				pageManager.DeletePage(Page.Id);
			}

			return Accepted();
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
			pageManager.DeletePage(id);

			return AcceptedAtAction(nameof(Get));
        }
    }
}
