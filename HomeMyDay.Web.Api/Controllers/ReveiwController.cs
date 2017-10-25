using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Api.Controllers
{
	//[Produces("application/json")]
 //   [Route("api/reviews")]
 //   public class ReviewController : Controller
 //   {
	//	private readonly IReviewManager ReviewManager;

	//	public ReviewController(IReviewManager ReviewMgr)
	//	{
	//		ReviewManager = ReviewMgr;
	//	}

	//	[HttpGet]
	//	public IEnumerable<Review> Get()
	//	{
	//		return ReviewManager.GetReviews();
	//	}

	//	// GET api/values
	//	[HttpGet("{id}")]
	//	public Review Get(int id)
 //       {
	//		return ReviewManager.GetReview(id);
 //       }

 //       // POST api/values
 //       [HttpPost]
 //       public IActionResult Post([FromBody]Review Review)
 //       {
	//		//ReviewManager.(Review);

	//		return CreatedAtAction(nameof(Get), new { id = Review.Id }, Review);
 //       }

	//	[HttpPut]
	//	public IActionResult Put([FromBody]Review[] countries)
	//	{
	//		foreach(Review review in countries)
	//		{
	//			ReviewManager.Save(review);
	//		}

	//		return Accepted();
	//	}

 //       // PUT api/values/5
 //       [HttpPut("{id}")]
 //       public IActionResult Put(int id, [FromBody]Review review)
 //       {
	//		if(review.Id != id)
	//		{
	//			return BadRequest();
	//		}

	//		ReviewManager.Save(review);

	//		return AcceptedAtAction(nameof(Get), new { id = review.Id }, review);
 //       }

	//	[HttpDelete]
	//	public IActionResult Delete()
	//	{
	//		IEnumerable<Review> Reviews = ReviewManager.GetReviews();
	//		foreach(Review Review in Reviews)
	//		{
	//			ReviewManager.Delete(Review.Id);
	//		}

	//		return Accepted();
	//	}

	//	// DELETE api/values/5
	//	[HttpDelete("{id}")]
 //       public IActionResult Delete(int id)
 //       {
	//		ReviewManager.Delete(id);

	//		return AcceptedAtAction(nameof(Get));
 //       }
 //   }
}
