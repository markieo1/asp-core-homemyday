using System.ComponentModel.DataAnnotations;

namespace HomeMyDay.Models
{
    public class Vacancie : BaseModel
    {
        public string JobTitle { get; set; }

        public string Company { get; set; }

        public string City { get; set; }

        public string AboutVacancy { get; set; }

        public string AboutFunction { get; set; }

        public string JobRequirements { get; set; }

        public string WeOffer { get; set; }
    }
}