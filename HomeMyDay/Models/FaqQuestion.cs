using System.ComponentModel.DataAnnotations.Schema;

namespace HomeMyDay.Models
{
    public class FaqQuestion : BaseModel
    {
        /// <summary>
        /// The id of the Category which is linked in the database.
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// The Question string of the FAQ
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// The Answer string of a question in the FAQ
        /// </summary>
        public string Answer { get; set; }

        [ForeignKey("CategoryId")]
        public virtual FaqCategory FaqCategory { get; set; }
    }
}