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
		public int Id { get; set; }

		/// <summary>
		/// The name of the accommodation.
		/// </summary>
		public string Name { get; set; }

        /// <summary>
        /// Optional: The continent from the world map
        /// </summary>
        public string Continent { get; set; }

        /// <summary>
        /// The country where the customer is staying
        /// </summary>
        public string Country { get; set; }
    }
}
