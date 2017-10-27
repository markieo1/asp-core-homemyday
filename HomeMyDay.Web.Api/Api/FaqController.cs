using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Models;
using Halcyon.HAL;
using Halcyon.Web.HAL;

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
		public IActionResult Get()
		{
			IEnumerable<FaqCategory> categories = faqManager.GetFaqCategories();

			//Generate a list of HALResponses
			var response = new List<HALResponse>();
			foreach (FaqCategory category in categories)
			{
				response.Add(
					new HALResponse(category)
					.AddLinks(new Link[] {
						new Link(Link.RelForSelf, $"/api/v1/faq/categories/{category.Id}"),
						new Link("questions", $"/api/v1/faq/categories/{category.Id}/questions", "Category Questions"),
						new Link("updateCategory", $"/api/v1/faq/categories/{category.Id}", "Update Category", "PUT"),
						new Link("deleteCategory", $"/api/v1/faq/categories/{category.Id}", "Delete Category", "DELETE")
					})
					.AddEmbeddedCollection("questions", category.Questions)
				);
			}

			return this.Ok(response);
		}

		// GET api/values
		[HttpGet("categories/{categoryId}")]
		public IActionResult Get(long categoryId)
		{
			FaqCategory category;

			try
			{
				category = faqManager.GetFaqCategory(categoryId);
			}
			catch(KeyNotFoundException)
			{
				return NotFound();
			}

			return this.HAL(category, new Link[] {
				new Link(Link.RelForSelf, $"/api/v1/faq/categories/{categoryId}"),
				new Link("questions", $"/api/v1/faq/categories/{categoryId}/questions", "Category Questions"),
				new Link("updateCategory", $"/api/v1/faq/categories/{categoryId}", "Update Category", "PUT"),
				new Link("deleteCategory", $"/api/v1/faq/categories/{categoryId}", "Delete Category", "DELETE"),
				new Link("createQuestion", $"api/v1/faq/categories/{categoryId}/questions", "Create Question", "POST")
			});
		}

		[HttpGet("categories/{categoryId}/questions")]
		public IActionResult GetQuestions(long categoryId)
		{
			IEnumerable<FaqQuestion> questions = faqManager.GetFaqQuestions(categoryId);
			var response = new List<HALResponse>();
			foreach(FaqQuestion question in questions)
			{
				response.Add(new HALResponse(question)
					.AddLinks(new Link[] {
						new Link(Link.RelForSelf, $"api/v1/faq/categories/{categoryId}/questions/{question.Id}"),
						new Link("createQuestion", $"api/v1/faq/categories/{categoryId}/questions", "Create Question", "POST"),
						new Link("updateQuestion", $"api/v1/faq/categories/{categoryId}/questions/{question.Id}", "Update Question", "PUT"),
						new Link("deleteQuestion", $"api/v1/faq/categories/{categoryId}/questions/{question.Id}", "Delete Question", "DELETE")
				}));
			}

			return this.Ok(response);
		}

		// GET api/values
		[HttpGet("categories/{categoryId}/questions/{questionId}")]
		public IActionResult Get(long categoryId, long questionId)
		{
			FaqQuestion question;

			try
			{
				question = faqManager.GetFaqQuestion(questionId);
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}

			if(question.Category.Id != categoryId)
			{
				return NotFound();
			}

			return this.HAL(question, new Link[] {
				new Link(Link.RelForSelf, $"api/v1/faq/categories/{categoryId}/questions/{question.Id}"),
				new Link("updateQuestion", $"api/v1/faq/categories/{categoryId}/questions/{question.Id}", "Update Question", "PUT"),
				new Link("deleteQuestion", $"api/v1/faq/categories/{categoryId}/questions/{question.Id}", "Delete Question", "DELETE")
			});
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

			return CreatedAtAction(nameof(Get), new { id = faqCategory.Id }, new HALResponse(faqCategory).AddLinks(new Link[] {
				new Link(Link.RelForSelf, $"api/v1/faq/categories/{faqCategory.Id}")
			}));
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

			return CreatedAtAction(nameof(Get), new { categoryId = faqQuestion.Category.Id, questionId = faqQuestion.Id }, new HALResponse(faqQuestion).AddLinks(new Link[] {
				new Link(Link.RelForSelf, $"api/v1/faq/categories/{faqQuestion.Category.Id}/questions/{faqQuestion.Id}")
			}));
		}

		[HttpPut("categories")]
		public async Task<IActionResult> Put([FromBody]FaqCategory[] faqCategories)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (FaqCategory faqcategory in faqCategories)
			{
				await faqManager.SaveCategory(faqcategory);
			}

			return AcceptedAtAction(nameof(Get));
		}

		[HttpPut("categories/{categoryId}/questions")]
		public async Task<IActionResult> Put(long categoryId, [FromBody] FaqQuestion[] faqQuestions)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (FaqQuestion faqquestion in faqQuestions)
			{
				await faqManager.SaveQuestion(faqquestion);
			}

			return AcceptedAtAction(nameof(Get), new { categoryId = categoryId });
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
				ModelState.AddModelError("idmismatch", "ID of category in request body does not match the requested ID.");
				return BadRequest(ModelState);
			}

			faqManager.SaveCategory(faqCategory);

			return AcceptedAtAction(nameof(Get), new { id = faqCategory.Id }, new HALResponse(faqCategory).AddLinks(new Link[] {
				new Link(Link.RelForSelf, $"api/v1/faq/categories/{faqCategory.Id}")
			}));
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

			return AcceptedAtAction(nameof(Get), new { id = faqQuestion.Category.Id, questionId = faqQuestion.Id }, new HALResponse(faqQuestion).AddLinks(new Link[] {
				new Link(Link.RelForSelf, $"api/v1/faq/categories/{faqQuestion.Category.Id}/questions/{faqQuestion.Id}")
			}));
		}

		[HttpDelete("categories")]
		public async Task<IActionResult> Delete()
		{
			IEnumerable<FaqCategory> faqcategories = faqManager.GetFaqCategories();
			foreach (FaqCategory faqcategory in faqcategories)
			{
				await faqManager.DeleteCategory(faqcategory.Id);
			}

			return AcceptedAtAction(nameof(Get));
		}

		[HttpDelete("categories/{categoryId}/questions")]
		public async Task<IActionResult> Delete(long categoryId)
		{
			IEnumerable<FaqQuestion> faqQuestions = faqManager.GetFaqQuestions(categoryId);
			foreach (FaqQuestion faqQuestion in faqQuestions)
			{
				await faqManager.DeleteQuestion(faqQuestion.Id);
			}

			var category = faqManager.GetFaqCategory(categoryId);

			return AcceptedAtAction(nameof(Get), new { categoryId = categoryId }, new HALResponse(category).AddLinks(new Link[] {
				new Link(Link.RelForSelf, $"/api/v1/faq/categories/{categoryId}"),
				new Link("questions", $"/api/v1/faq/categories/{categoryId}/questions", "Category Questions"),
				new Link("updateCategory", $"/api/v1/faq/categories/{categoryId}", "Update Category", "PUT"),
				new Link("deleteCategory", $"/api/v1/faq/categories/{categoryId}", "Delete Category", "DELETE"),
				new Link("createQuestion", $"api/v1/faq/categories/{categoryId}/questions", "Create Question", "POST")
			}));
		}

		// DELETE api/values/5
		[HttpDelete("categories/{categoryId}")]
		public IActionResult DeleteCategories(long categoryId)
		{
			faqManager.DeleteCategory(categoryId);

			return AcceptedAtAction(nameof(Get));
		}

		[HttpDelete("categories/{categoryId}/questions/{questionId}")]
		public IActionResult Delete(long categoryId, long questionId)
		{
			faqManager.DeleteQuestion(questionId);

			var category = faqManager.GetFaqCategory(categoryId);

			return AcceptedAtAction(nameof(Get), new { categoryId = categoryId }, new HALResponse(category).AddLinks(new Link[] {
				new Link(Link.RelForSelf, $"/api/v1/faq/categories/{categoryId}"),
				new Link("questions", $"/api/v1/faq/categories/{categoryId}/questions", "Category Questions"),
				new Link("updateCategory", $"/api/v1/faq/categories/{categoryId}", "Update Category", "PUT"),
				new Link("deleteCategory", $"/api/v1/faq/categories/{categoryId}", "Delete Category", "DELETE"),
				new Link("createQuestion", $"api/v1/faq/categories/{categoryId}/questions", "Create Question", "POST")
			}));
		}
	}
}
