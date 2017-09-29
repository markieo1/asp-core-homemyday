using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HomeMyDay.Models
{
    public class BookingPerson : BaseModel
    {
		/// <summary>
		/// Whether this person is the owner of the booking.
		/// </summary>
		public bool BookingOwner { get; set; }

		/// <summary>
		/// The salutation of the person (mr/mrs)
		/// </summary>
		[DisplayName("Aanhef")]
		[Required]
		public string Salutation { get; set; }

		/// <summary>
		/// The person's initials.
		/// </summary>
		[DisplayName("Voorletters")]
		[Required]
		public string Initials { get; set; }

		/// <summary>
		/// The person's first name.
		/// </summary>
		[DisplayName("Voornaam")]
		[Required]
		public string FirstName { get; set; }

		/// <summary>
		/// The person's insertion (tussenvoegsel).
		/// </summary>
		[DisplayName("Tussenvoegsel")]
		[Required]
		public string Insertion { get; set; }

		/// <summary>
		/// The person's last name.
		/// </summary>
		[DisplayName("Achternaam")]
		[Required]
		public string LastName { get; set; }

		/// <summary>
		/// The date the person was born.
		/// </summary>
		[DisplayName("Geboortedatum")]
		[Required]
		public DateTime BirthDate { get; set; }

		/// <summary>
		/// The person's email address.
		/// </summary>
		[DisplayName("E-mailadres")]
		[Required]
		public string Email { get; set; }

		/// <summary>
		/// The country the person resides in.
		/// </summary>
		[DisplayName("Land")]
		[Required]
		public Country Country { get; set; }

		/// <summary>
		/// The postal code that the person resides in.
		/// </summary>
		[DisplayName("Postcode")]
		[Required]
		public string PostalCode { get; set; }

		/// <summary>
		/// The house number of the person.
		/// </summary>
		[DisplayName("Huisnummer")]
		[Required]
		public int HouseNumber { get; set; }

		/// <summary>
		/// The house number suffix of the person.
		/// </summary>
		[DisplayName("Toevoeging")]
		public string HouseNumberSuffix { get; set; }

		/// <summary>
		/// The person's phone number.
		/// </summary>
		[DisplayName("Telefoonnummer")]
		[Required]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// The country that the person is from.
		/// </summary>
		[DisplayName("Nationaliteit")]
		[Required]
		public Country Nationality { get; set; }

		/// <summary>
		/// The amount of baggage that the person will be taking.
		/// </summary>
		[DisplayName("Bagage")]
		public string Baggage { get; set; }
    }
}
