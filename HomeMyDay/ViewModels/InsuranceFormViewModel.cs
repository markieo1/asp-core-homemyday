using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.ViewModels
{
    public class InsuranceFormViewModel
    {
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
