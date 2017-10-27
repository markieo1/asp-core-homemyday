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
		public FaqCategory Get(long categoryId)
		{
			return faqManager.GetFaqCategory(categoryId);
		}

		[HttpGet("categories/{categoryId}/questions")]
		public IEnumerable<FaqQuestion> GetQuestions(long categoryId)
		{
			return faqManager.GetFaqQuestions(categoryId) ?? Enumerable.Empty<FaqQuestion>();
		}

		// GET api/values
		[HttpGet("categories/{categoryId}/questions/{questionId}")]
		public FaqQuestion Get(long categoryId, long questionId)
		{
			return faqManager.GetFaqQuestion(questionId);
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

			return CreatedAtAction(nameof(Get), new { id = faqQuestion.Category.Id, questionId = faqQuestion.Id }, faqQuestion);
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

			return Accepted();
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

			return Accepted();
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

			return AcceptedAtAction(nameof(Get), new { id = faqCategory.Id }, faqCategory);
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

			return AcceptedAtAction(nameof(Get), new { id = faqQuestion.Category.Id, questionId = faqQuestion.Id }, faqQuestion);
		}

		[HttpDelete("categories")]
		public async Task<IActionResult> Delete()
		{
			IEnumerable<FaqCategory> faqcategories = faqManager.GetFaqCategories();
			foreach (FaqCategory faqcategory in faqcategories)
			{
				await faqManager.DeleteCategory(faqcategory.Id);
			}

			return Accepted();
		}

		[HttpDelete("categories/{categoryId}/questions")]
		public async Task<IActionResult> Delete(long categoryId)
		{
			IEnumerable<FaqQuestion> faqQuestions = faqManager.GetFaqQuestions(categoryId);
			foreach (FaqQuestion faqQuestion in faqQuestions)
			{
				await faqManager.DeleteQuestion(faqQuestion.Id);
			}

			return Accepted();
		}

		// DELETE api/values/5
		[HttpDelete("categories/{categoryId}")]
		public IActionResult DeleteCategories(long categoryId)
		{
			faqManager.DeleteCategory(categoryId);

			return AcceptedAtAction(nameof(Get));
		}

		// PUT api/values/5
		[HttpDelete("categories/{categoryId}/questions/{questionId}")]
		public IActionResult Delete(long categoryId, long questionId)
		{
			faqManager.DeleteQuestion(categoryId);

			return AcceptedAtAction(nameof(Get));
		}
	}
}
