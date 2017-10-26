using System.Collections.Generic;

namespace HomeMyDay.Core.Models
{
    public class FaqCategory : BaseModel
    {
        /// <summary>
        /// The categrory of faq which saved in the database.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// List of questions which belongs to a category
        /// </summary>
        public List<FaqQuestion> Questions {get; set;}
    }
}
