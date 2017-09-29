using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.ViewModels
{
    public class VacancieViewModel
    {
        /// <summary>
        /// Key for the Database 
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// The jobtitle of the vacancy
        /// </summary>
        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string AboutVacancy { get; set; }

        [Required]
        public string AboutFunction { get; set; }

        [Required]
        public string JobRequirements { get; set; }

        [Required]
        public string WeOffer { get; set; }
    }
}
