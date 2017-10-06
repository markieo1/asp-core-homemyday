using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.ViewModels
{
    public class VacancyViewModel
    {
        /// <summary>
        /// The job-title of the vacancie
        /// </summary>
        [Required]
        public string JobTitle { get; set; }

        /// <summary>
        /// The company of the vacancie is
        /// </summary>
        [Required]
        public string Company { get; set; }

        /// <summary>
        /// The city where the vacancie is
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Text about the company and the vacancy
        /// </summary>
        [Required]
        public string AboutVacancy { get; set; }

        /// <summary>
        /// The abouttext of the vacancie
        /// </summary>
        [Required]
        public string AboutFunction { get; set; }

        /// <summary>
        /// The requirements of the function
        /// </summary>
        [Required]
        public string JobRequirements { get; set; }

        /// <summary>
        /// The whatWeOffer text of the vacancie
        /// </summary>
        [Required]
        public string WeOffer { get; set; }
    }
}