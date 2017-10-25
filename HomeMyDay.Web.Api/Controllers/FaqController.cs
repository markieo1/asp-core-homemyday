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
	[Route("api/faq")]
	public class FaqController : Controller
	{
		private readonly IFaqManager faqManager;

		public FaqController(IFaqManager faqMgr)
		{
			faqManager = faqMgr;
		}

		[HttpGet]
		public IEnumerable<FaqCategory> Get()
		{
			return faqManager.GetFaqCategories();
		}

		[HttpGet("category/{id}/questions")]
		public IEnumerable<FaqQuestion> Get(long id)
		{
			return faqManager.GetFaqQuestions(id);
		}

		// GET api/values
		[HttpGet("category/{id}")]
		public FaqCategory Get(int id)
		{
			return faqManager.GetFaqCategory(id);
		}

		// GET api/values
		[HttpGet("category/{id}/questions/{questionid}")]
		public FaqQuestion Get(int id, int questionid)
		{
			return faqManager.GetFaqQuestion(id, questionid);
		}

		// POST api/values
		[HttpPost("category")]
		public IActionResult Post([FromBody]FaqCategory faqcategory)
		{
			faqManager.SaveCategory(faqcategory);

			return CreatedAtAction(nameof(Get), new { id = faqcategory.Id }, faqcategory);
		}

		// POST api/values
		[HttpPost("category/{id}/questions")]
		public IActionResult Post(long id, [FromBody] FaqQuestion faqquestion)
		{
			faqManager.SaveQuestion(faqquestion);

			return CreatedAtAction(nameof(Get), new { id = faqquestion.CategoryId, questionid = faqquestion.Id }, faqquestion);
		}

		[HttpPut("category")]
		public IActionResult Put([FromBody]FaqCategory[] faqcategories)
		{
			foreach (FaqCategory faqcategory in faqcategories)
			{
				faqManager.SaveCategory(faqcategory);
			}

			return Accepted();
		}

		[HttpPut("category/{id}/questions")]
		public IActionResult Put(long id, [FromBody] FaqQuestion[] faqquestions)
		{
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
			if(faqcategory.Id != id)
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
			if (faqQuestion.CategoryId != id && faqQuestion.Id != questionid)
			{
				return BadRequest();
			}

			faqManager.SaveQuestion(faqQuestion);

			return AcceptedAtAction(nameof(Get), new { id = faqQuestion.CategoryId, questionid = faqQuestion.Id }, faqQuestion);
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
