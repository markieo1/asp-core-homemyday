﻿using System.Collections.Generic;

namespace HomeMyDay.Models
{
    public class FaqCategory : BaseModel
    {
        public FaqCategory()
        {
            FaqQuestions = new List<FaqQuestion>();
        }

        /// <summary>
        /// The categrory of faq which saved in the database.
        /// </summary>
        public string CategoryName { get; set; }

        public List<FaqQuestion> FaqQuestions {get; set;}
    }
}
