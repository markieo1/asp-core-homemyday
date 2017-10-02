using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.ViewModels
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
        /// The date of when the review is placed
        /// </summary>
        [Required]
        public DateTime Date { get; set; }
    }
}
