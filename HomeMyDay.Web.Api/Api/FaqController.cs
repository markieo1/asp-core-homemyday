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
		[HttpGet("categories/{categoryId}")]
		public IActionResult Get(long categoryId)
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
			return faqManager.GetFaqQuestions(categoryId) ?? Enumerable.Empty<FaqQuestion>();
		}

		// GET api/values
		[HttpGet("categories/{categoryId}/questions/{questionId}")]
		public IActionResult Get(long categoryId, long questionId)
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
		public IActionResult Post([FromBody]FaqCategory faqCategory)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			faqManager.SaveCategory(faqCategory);

			return CreatedAtAction(nameof(Get), new { id = faqCategory.Id }, faqCategory);
		}

		// POST api/values
		[HttpPost("categories/{categoryId}/questions")]
		public IActionResult Post(long id, [FromBody] FaqQuestion faqQuestion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			faqManager.SaveQuestion(faqQuestion);

			return CreatedAtRoute(nameof(Get), new { id = faqquestion.Category.Id, questionid = faqquestion.Id }, faqquestion);
		}

		[HttpPut("categories")]
		public IActionResult Put([FromBody]FaqCategory[] faqCategories)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (FaqCategory faqcategory in faqCategories)
			{
				faqManager.SaveCategory(faqcategory);
			}

			return Ok(faqcategories);
		}

		[HttpPut("categories/{categoryId}/questions")]
		public IActionResult Put(long id, [FromBody] FaqQuestion[] faqQuestions)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (FaqQuestion faqquestion in faqQuestions)
			{
				faqManager.SaveQuestion(faqquestion);
			}

			return Ok(faqquestions);
		}

		// PUT api/values/5
		[HttpPut("categories/{categoryId}")]
		public IActionResult Put(long categoryId, [FromBody]FaqCategory faqCategory)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (faqCategory.Id != categoryId)
			{
				return BadRequest();
			}

			faqManager.SaveCategory(faqCategory);

	        return Ok(faqcategory);
        }

		// PUT api/values/5
		[HttpPut("categories/{categoryId}/questions/{questionId}")]
		public IActionResult Put(long categoryId, long questionId, [FromBody]FaqQuestion faqQuestion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (faqQuestion.Category.Id != categoryId && faqQuestion.Id != questionId)
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
			foreach (FaqCategory faqcategory in faqcategories)
			{
				await faqManager.DeleteCategory(faqcategory.Id);
			}

			return NoContent();
		}

		[HttpDelete("categories/{categoryId}/questions")]
		public async Task<IActionResult> Delete(long categoryId)
		{
			IEnumerable<FaqQuestion> faqQuestions = faqManager.GetFaqQuestions(categoryId);
			foreach (FaqQuestion faqQuestion in faqQuestions)
			{
				await faqManager.DeleteQuestion(faqQuestion.Id);
			}

			return NoContent();
		}

		// DELETE api/values/5
		[HttpDelete("categories/{categoryId}")]
		public IActionResult DeleteCategories(long categoryId)
		{
			faqManager.DeleteCategory(categoryId);

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
		[HttpDelete("categories/{categoryId}/questions/{questionId}")]
		public IActionResult Delete(long categoryId, long questionId)
		{
			faqManager.DeleteQuestion(categoryId);

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
