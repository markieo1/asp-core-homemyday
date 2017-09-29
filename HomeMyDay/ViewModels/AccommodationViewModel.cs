using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.ViewModels
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
		/// <returns></returns>
		public static AccommodationViewModel FromAccommodation(Accommodation accommodation)
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
					}
				}
			};
		}
	}
}
