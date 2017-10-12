using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.Core.Models
{
    public class Vacancy : BaseModel
    {
        /// <summary>
        /// The job-title of the vacancie
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// The company of the vacancie is
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// The city where the vacancie is
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Text about the company and the vacancy
        /// /// </summary>
        public string AboutVacancy { get; set; }

        /// <summary>
        /// The abouttext of the vacancie
        /// </summary>
        public string AboutFunction { get; set; }

        /// <summary>
        /// The requirements of the function
        /// </summary>
        public string JobRequirements { get; set; }

        /// <summary>
        /// The whatWeOffer text of the vacancie
        /// </summary>
        public string WeOffer { get; set; }
    }
}