using HomeMyDay.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace HomeMyDay.Web.Base.Home.ViewModels
{
	public class AccommodationViewModel
	{
		/// <summary>
		/// Gets or sets the accommodation.
		/// </summary>
		/// <value>
		/// The accommodation.
		/// </value>
		public Accommodation Accommodation { get; set; }

		/// <summary>
		/// Gets reviews of an accomodation.
		/// </summary>
		/// <value>
		/// The reviews.
		/// </value>
		public IEnumerable<ReviewViewModel> Reviews { get; set; }

		/// <summary>
		/// Gets or sets the detail blocks.
		/// </summary>
		/// <value>
		/// The detail blocks.
		/// </value>
		public IEnumerable<AccommodationDetailBlockViewModel> DetailBlocks { get; set; }

		/// <summary>
		/// Creates an instance of <see cref="AccommodationViewModel"/> by using the <paramref name="accommodation"/>
		/// </summary>
		/// <param name="accommodation">The accommodation.</param>
		/// <param name="reviews">The reviews of the accommodation</param>
		/// <returns></returns>
		public static AccommodationViewModel FromAccommodation(Accommodation accommodation, List<Review> reviews)
		{
			return new AccommodationViewModel()
			{
				Accommodation = accommodation,

				DetailBlocks = new List<AccommodationDetailBlockViewModel>
				{
					new AccommodationDetailBlockViewModel()
					{
						Title = "Ruimte",
						Text = accommodation.SpaceText
					},
					new AccommodationDetailBlockViewModel()
					{
						Title = "Voorzieningen",
						Text = accommodation.ServicesText
					},
					new AccommodationDetailBlockViewModel()
					{
						Title = "Prijzen",
						Text = accommodation.PricesText
					},
					new AccommodationDetailBlockViewModel()
					{
						Title = "Huisregels",
						Text = accommodation.RulesText
					},
					new AccommodationDetailBlockViewModel()
					{
						Title = "Annulering",
						Text = accommodation.CancellationText
					},
				},
				Reviews = reviews.Select(x => new ReviewViewModel()
				{
					Title = x.Title,
					Name = x.Name,
					Text = x.Text,
					Date = x.Date
				})
			};
		}
	}
}
