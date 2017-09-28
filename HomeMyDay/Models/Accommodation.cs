﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Models
{
	public class Accommodation : BaseModel
	{
		/// <summary>
		/// The user-friendly name of the accommodation.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The user-friendly description of the accommodation.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The maximum number of people that this accommodation supports.
		/// </summary>
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
		public int? Rooms { get; set; }

		/// <summary>
		/// Optional: The amount of beds available in total of all the rooms
		/// </summary>
		public int? Beds { get; set; }

		/// <summary>
		/// Gets or sets the media objects.
		/// </summary>
		public List<MediaObject> MediaObjects { get; set; }

		/// <summary>
		/// Recommended can be set true or false to see if a holiday is recommended for the user
		/// </summary>
		public bool Recommended { get; set; }

		/// <summary>
		/// The price for the accommodation.
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// Gets or sets the not available dates.
		/// </summary>
		public List<DateEntity> NotAvailableDates { get; set; }
	}
}
