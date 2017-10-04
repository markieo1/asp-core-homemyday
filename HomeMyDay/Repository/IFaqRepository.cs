using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Repository
{
    public interface IFaqRepository
    {
        /// <summary>
		/// Get All Categories and Questions 
		/// </summary>
		/// <returns>The categories and the linked questions to a categorie</returns>
        IEnumerable<FaqCategory> GetCategoriesAndQuestions();
    }
}
