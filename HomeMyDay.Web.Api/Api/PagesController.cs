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
		public IActionResult Get(int id)
        {
	        var result = pageManager.GetPage(id);

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
        public IActionResult Post([FromBody]Page page)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			pageManager.AddPage(page);

			return CreatedAtAction(nameof(Get), new { id = page.Id }, page);
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

			return Ok(pages);
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

			return Ok(page);
        }

		[HttpDelete]
		public IActionResult Delete()
		{
			IEnumerable<Page> pages = pageManager.GetPages();
			foreach(Page page in pages)
			{
				pageManager.DeletePage(page.Id);
			}

			return NoContent();
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
			pageManager.DeletePage(id);

	        if (pageManager.GetPage(id) == null)
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
