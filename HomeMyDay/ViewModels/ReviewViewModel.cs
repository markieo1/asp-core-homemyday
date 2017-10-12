using System;
using System.ComponentModel.DataAnnotations;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Home.ViewModels
{
	public class ReviewViewModel
	{
		/// <summary>
		/// The Name of the user who filled in the form
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The Name of the user who filled in the form
		/// </summary>
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// The review text
		/// </summary>
		[Required]
		public string Text { get; set; }

		/// <summary>
		/// The status of the review
		/// </summary>
		public bool Approved { get; set; }

		/// <summary>
		/// The date of when the review is placed
		/// </summary>
		[Required]
		public DateTime Date { get; set; }

		/// <summary>
		/// The id of the accommodation
		/// </summary>
		public long AccommodationId { get; set; }

		/// <summary>
		/// Creates an instance of <see cref="ReviewViewModel"/> by using the <paramref name="review"/>
		/// </summary>
		/// <param name="review">The reviews of the accommodation</param>
		/// <returns></returns>
		public static ReviewViewModel FromReview(Review review)
		{
			return new ReviewViewModel()
			{										   
				Name = review.Name,
				Date = review.Date,
				Title = review.Title,
				Text = review.Text,
				Approved = review.Approved
			};
		}
	}
}
