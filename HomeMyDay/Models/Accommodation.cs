﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Models
{
	public class Accommodation
	{
		/// <summary>
		/// The ID of the Accommodation. Automatically generated by the database.
		/// </summary>
		[Key]
		public long Id { get; set; }

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
		public IEnumerable<MediaObject> MediaObjects { get; set; }
	}
}
