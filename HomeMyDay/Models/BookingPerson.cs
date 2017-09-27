using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Models
{
    public class BookingPerson
    {
		/// <summary>
		/// The ID of the Person. Database-generated.
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Whether this person is the owner of the booking.
		/// </summary>
		public bool BookingOwner { get; set; }

		/// <summary>
		/// The salutation of the person (mr/mrs)
		/// </summary>
		public string Salutation { get; set; }

		/// <summary>
		/// The person's initials.
		/// </summary>
		public string Initials { get; set; }

		/// <summary>
		/// The person's first name.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// The person's insertion (tussenvoegsel).
		/// </summary>
		public string Insertion { get; set; }

		/// <summary>
		/// The person's last name.
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// The date the person was born.
		/// </summary>
		public DateTime BirthDate { get; set; }

		/// <summary>
		/// The person's email address.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// The country the person resides in.
		/// </summary>
		public Country Country { get; set; }

		/// <summary>
		/// The postal code that the person resides in.
		/// </summary>
		public string PostalCode { get; set; }

		/// <summary>
		/// The house number of the person.
		/// </summary>
		public int HouseNumber { get; set; }

		/// <summary>
		/// The house number suffix of the person.
		/// </summary>
		public string HouseNumberSuffix { get; set; }

		/// <summary>
		/// The person's phone number.
		/// </summary>
		public string PhoneNumber { get; set; }

		/// <summary>
		/// The country that the person is from.
		/// </summary>
		public Country Nationality { get; set; }
    }
}
