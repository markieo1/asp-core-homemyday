using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Api.Controllers
{
    public class ReviewsController : BaseApiController
    {
		private readonly IReviewManager reviewManager;

		public ReviewsController(IReviewManager reviewMgr)
		{
			reviewManager = reviewMgr;
		}

		[HttpGet]
		public IEnumerable<Review> Get()
		{
			return reviewManager.GetAllReviews();
		}

		// GET api/values
		[HttpGet("{id}")]
		public IActionResult Get(int id)
        {

	        var result = reviewManager.GetReview(id);

			//check if id is a integer
			//var isNum = int.TryParse(id.ToString(), out var n);

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
        public IActionResult Post([FromBody]Review review)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			reviewManager.AddReview(review.Accommodation.Id, review.Title, review.Name, review.Text);

			return CreatedAtAction(nameof(Get), new { id = review.Id }, review);
        }

		[HttpPut]
		public IActionResult Put([FromBody]Review[] countries)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (Review review in countries)
			{
				reviewManager.Save(review);
			}

			return Ok(countries);
		}

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]Review review)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (review.Id != id)
			{
				return BadRequest();
			}

			reviewManager.Save(review);

			return Ok(review);
        }

		[HttpDelete]
		public IActionResult Delete()
		{
			IEnumerable<Review> reviews = reviewManager.GetAllReviews();
			foreach(Review review in reviews)
			{
				reviewManager.Delete(review.Id);
			}

			return NoContent();
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
			reviewManager.Delete(id);

	        if (reviewManager.GetReview(id) == null)
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
