using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Core.Models
{
	public class Booking : BaseModel
	{
		/// <summary>
		/// The list of people who are traveling.
		/// </summary>
		public List<BookingPerson> Persons { get; set; }

		/// <summary>
		/// The accommodation where the customer is staying.
		/// </summary>
		public Accommodation Accommodation { get; set; }

		/// <summary>
		/// The departure date of the flight.
		/// </summary>
		public DateTime DepartureDate { get; set; }

		/// <summary>
		/// The departure date of the return flight.
		/// </summary>
		public DateTime ReturnDate { get; set; }

		/// <summary>
		/// Whether the user has chosen for Service Insurance.
		/// </summary>
		[DisplayName("Service 2.0")]
		public bool InsuranceService { get; set; }

		/// <summary>
		/// Whether the user has chosen for the Explore service.
		/// </summary>
		[DisplayName("Explore 2.0")]
		public bool InsuranceExplore { get; set; }

		/// <summary>
		/// Whether the user has chosen for the basic cancellation insurance.
		/// </summary>
		[DisplayName("Basisverzekering Annuleren")]
		public bool InsuranceCancellationBasic { get; set; }

		/// <summary>
		/// Whether the user has chosen for the all-risk cancellation insurance.
		/// </summary>
		[DisplayName("All Risk Annuleren")]
		public bool InsuranceCancellationAllRisk { get; set; }

		/// <summary>
		/// The type of insurance that the user has chosen.
		/// </summary>
		public InsuranceType InsuranceType { get; set; }

		/// <summary>
		/// Whether the customer wants to be transferred from their house to the airport.
		/// </summary>
		[DisplayName("Transfer van huis naar vliegveld")]
		public bool TransferToAirport { get; set; }

		/// <summary>
		/// Whether the customer wants to be transferred from the airport to their house (on the return trip).
		/// </summary>
		[DisplayName("Transfer van vliegveld naar huis")]
		public bool TransferFromAirport { get; set; }
	}
}
