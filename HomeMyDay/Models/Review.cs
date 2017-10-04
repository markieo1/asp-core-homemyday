using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeMyDay.Models
{
    public class Review : BaseModel
    {
        /// <summary>
		/// The accommodation where the customer is staying.
		/// </summary>
        
		public long AccommodationId { get; set; }

        /// <summary>
        /// The Name of the user who filled in the form
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The Name of the user who filled in the form
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The review text
        /// </summary>
        public  string Text { get; set; }

        /// <summary>
        /// The date of when the review is placed
        /// </summary>
        public DateTime Date { get; set; }

        [ForeignKey("AccommodationId")]
        public virtual Accommodation Accommodation { get; set; }
    }
}
