﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Models
{
    public class Holiday
    {
        /// <summary>
        /// Key for the Database 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The destination where the customer is going
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// The category is for multiple choices like: houses, experiences, places
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Recommended can be set true or false to see if a holiday is recommended for the user
        /// </summary>
        public bool Recommended { get; set; }

        /// <summary>
        /// Optional: The amount of rooms available during the holiday
        /// </summary>
        public int Room { get; set; }

        /// <summary>
        /// Optional: The amount of beds available in total of all the rooms
        /// </summary>
        public int Bed { get; set; }

        /// <summary>
        /// The price for the holiday
        /// </summary>
        public decimal Price { get; set; }
    }
}
