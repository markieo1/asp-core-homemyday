using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Models
{
	public class Accommodation : BaseModel
	{
		public Accommodation()
		{
			MediaObjects = new List<MediaObject>();
			NotAvailableDates = new List<DateEntity>();
		}

		/// <summary>
		/// The user-friendly name of the accommodation.
		/// </summary>
		[Required]
		[Display(Name = "Naam")]
		public string Name { get; set; }

		/// <summary>
		/// The user-friendly description of the accommodation.
		/// </summary>
		[Required]
		[Display(Name = "Beschrijving")]
		public string Description { get; set; }

		/// <summary>
		/// The maximum number of people that this accommodation supports.
		/// </summary>
		[Required]
		[Display(Name = "Maximaal aantal personen")]
		public int MaxPersons { get; set; }

		/// <summary>
		/// Optional: The continent from the world map
		/// </summary>
		public string Continent { get; set; }

		/// <summary>
		/// The country where the customer is staying
		/// </summary>
		public string Country { get; set; }

		/// <summary>
		/// The location string. Usually the city name.
		/// </summary>
		public string Location { get; set; }

		/// <summary>
		/// Optional: The amount of rooms available during the holiday
		/// </summary>
		[Required]
		[Display(Name = "Kamers")]
		public int? Rooms { get; set; }

		/// <summary>
		/// Optional: The amount of beds available in total of all the rooms
		/// </summary>
		[Required]
		[Display(Name = "Bedden")]
		public int? Beds { get; set; }

		/// <summary>
		/// Gets or sets the media objects.
		/// </summary>
		public List<MediaObject> MediaObjects { get; set; }

		/// <summary>
		/// Recommended can be set true or false to see if a holiday is recommended for the user
		/// </summary>
		[Display(Name = "Aanbevolen")]
		public bool Recommended { get; set; }

		/// <summary>
		/// The price for the accommodation.
		/// </summary>
		[Required]
		[Display(Name = "Prijs")]
		public decimal Price { get; set; }

		/// <summary>
		/// Gets or sets the not available dates.
		/// </summary>
		public List<DateEntity> NotAvailableDates { get; set; }

		/// <summary>
		/// Gets or sets the space text.
		/// </summary>
		[Display(Name = "Ruimte")]
		public string SpaceText { get; set; }

		/// <summary>
		/// Gets or sets the services text.
		/// </summary>
		[Display(Name = "Voorzieningen")]
		public string ServicesText { get; set; }

		/// <summary>
		/// Gets or sets the prices text.
		/// </summary>
		[Display(Name = "Prijzen")]
		public string PricesText { get; set; }

		/// <summary>
		/// Gets or sets the house rules text.
		/// </summary>
		[Display(Name = "Huisregels")]
		public string RulesText { get; set; }

		/// <summary>
		/// Gets or sets the cancellation text.
		/// </summary>
		[Display(Name = "Annulering")]
		public string CancellationText { get; set; }
	}
}
