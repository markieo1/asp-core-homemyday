using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Api.Controllers
{
	public class FaqController : BaseApiController
	{
		private readonly IFaqManager faqManager;

		public FaqController(IFaqManager faqMgr)
		{
			faqManager = faqMgr;
		}

		[HttpGet("categories")]
		public IEnumerable<FaqCategory> Get()
		{
			var result = faqManager.GetFaqCategories();

			return result;
		}

		// GET api/values
		[HttpGet("categories/{id}")]
		public IActionResult Get(int id)
		{
			var result = faqManager.GetFaqCategory(id);

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

		[HttpGet("categories/{id}/questions")]
		public IEnumerable<FaqQuestion> Get(long id)
		{
			return faqManager.GetFaqQuestions(id);
		}

		// GET api/values
		[HttpGet("categories/{id}/questions/{questionid}")]
		public IActionResult Get(int id, int questionid)
		{
			var result = faqManager.GetFaqQuestion(questionid);

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
		[HttpPost("categories")]
		public IActionResult Post([FromBody]FaqCategory faqcategory)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			faqManager.SaveCategory(faqcategory);

			return CreatedAtAction(nameof(Get), new { id = faqcategory.Id }, faqcategory);
		}

		// POST api/values
		[HttpPost("categories/{id}/questions")]
		public IActionResult Post(long id, [FromBody] FaqQuestion faqquestion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			faqManager.SaveQuestion(faqquestion);

			return CreatedAtRoute(nameof(Get), new { id = faqquestion.Category.Id, questionid = faqquestion.Id }, faqquestion);
		}

		[HttpPut("categories")]
		public IActionResult Put([FromBody]FaqCategory[] faqcategories)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (FaqCategory faqcategory in faqcategories)
			{
				faqManager.SaveCategory(faqcategory);
			}

			return Ok(faqcategories);
		}

		[HttpPut("categories/{id}/questions")]
		public IActionResult Put(long id, [FromBody] FaqQuestion[] faqquestions)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (FaqQuestion faqquestion in faqquestions)
			{
				faqManager.SaveQuestion(faqquestion);
			}

			return Ok(faqquestions);
		}

		// PUT api/values/5
		[HttpPut("categories/{id}")]
        public IActionResult Put(int id, [FromBody]FaqCategory faqcategory)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (faqcategory.Id != id)
			{
				return BadRequest();
			}

			faqManager.SaveCategory(faqcategory);

	        return Ok(faqcategory);
        }

		// PUT api/values/5
		[HttpPut("categories/{id}/questions/{questionid}")]
		public IActionResult Put(int id, int questionid, [FromBody]FaqQuestion faqQuestion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (faqQuestion.Category.Id != id && faqQuestion.Id != questionid)
			{
				return BadRequest();
			}

			faqManager.SaveQuestion(faqQuestion);

			return Ok(faqQuestion);
		}
		
		[HttpDelete("categories")]
		public async Task<IActionResult> Delete()
		{
			IEnumerable<FaqCategory> faqcategories = faqManager.GetFaqCategories();
			foreach(FaqCategory faqcategory in faqcategories)
			{
				await faqManager.DeleteCategory(faqcategory.Id);
			}

			return NoContent();
		}

		[HttpDelete("categories/{id}/questions")]
		public async Task<IActionResult> Delete(long id)
		{
			IEnumerable<FaqQuestion> faqQuestions = faqManager.GetFaqQuestions(id);
			foreach (FaqQuestion faqQuestion in faqQuestions)
			{
				await faqManager.DeleteQuestion(faqQuestion.Id);
			}

			return NoContent();
		}

		// DELETE api/values/5
		[HttpDelete("categories/{id}")]
        public IActionResult Delete(int id)
        {
			faqManager.DeleteCategory(id);

	        if (faqManager.GetFaqCategory(id) == null)
	        {
		        return NotFound(id);
	        }

	        if (!ModelState.IsValid)
	        {
		        return BadRequest(ModelState);
	        }

	        return NoContent();
        }

		// PUT api/values/5
		[HttpDelete("categories/{id}/questions/{questionid}")]
		public IActionResult Delete(int id, int questionid)
		{
			faqManager.DeleteQuestion(questionid);

			if (faqManager.GetFaqQuestion(id) == null)
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
