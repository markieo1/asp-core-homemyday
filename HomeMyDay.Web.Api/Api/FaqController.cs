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

		[HttpGet]
		public IEnumerable<FaqCategory> Get()
		{
			var result = faqManager.GetFaqCategories();

			return result;
		}

		// GET api/values
		[HttpGet("categories/{id}")]
		public FaqCategory Get(int id)
		{
			return faqManager.GetFaqCategory(id);
		}

		[HttpGet("categories/{id}/questions")]
		public IEnumerable<FaqQuestion> Get(long id)
		{
			return faqManager.GetFaqQuestions(id);
		}

		// GET api/values
		[HttpGet("categories/{id}/questions/{questionid}")]
		public FaqQuestion Get(int id, int questionid)
		{
			return faqManager.GetFaqQuestion(questionid);
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
		[HttpPost("category/{id}/questions")]
		public IActionResult Post(long id, [FromBody] FaqQuestion faqquestion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			faqManager.SaveQuestion(faqquestion);

			return CreatedAtAction(nameof(Get), new { id = faqquestion.Category.Id, questionid = faqquestion.Id }, faqquestion);
		}

		[HttpPut("category")]
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

			return Accepted();
		}

		[HttpPut("category/{id}/questions")]
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

			return Accepted();
		}

		// PUT api/values/5
		[HttpPut("category/{id}")]
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

			return AcceptedAtAction(nameof(Get), new { id = faqcategory.Id }, faqcategory);
        }

		// PUT api/values/5
		[HttpPut("category/{id}/questions/{questionid}")]
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

			return AcceptedAtAction(nameof(Get), new { id = faqQuestion.Category.Id, questionid = faqQuestion.Id }, faqQuestion);
		}
		
		[HttpDelete("category")]
		public async Task<IActionResult> Delete()
		{
			IEnumerable<FaqCategory> faqcategories = faqManager.GetFaqCategories();
			foreach(FaqCategory faqcategory in faqcategories)
			{
				await faqManager.DeleteCategory(faqcategory.Id);
			}

			return Accepted();
		}

		[HttpDelete("category/{id}/questions")]
		public async Task<IActionResult> Delete(long id)
		{
			IEnumerable<FaqQuestion> faqQuestions = faqManager.GetFaqQuestions(id);
			foreach (FaqQuestion faqQuestion in faqQuestions)
			{
				await faqManager.DeleteQuestion(faqQuestion.Id);
			}

			return Accepted();
		}

		// DELETE api/values/5
		[HttpDelete("category/{id}")]
        public IActionResult Delete(int id)
        {
			faqManager.DeleteCategory(id);

			return AcceptedAtAction(nameof(Get));
        }

		// PUT api/values/5
		[HttpDelete("category/{id}/questions/{questionid}")]
		public IActionResult Delete(int id, int questionid)
		{
			faqManager.DeleteQuestion(questionid);

			return AcceptedAtAction(nameof(Get));
		}
	}
}
