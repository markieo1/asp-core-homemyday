using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMyDay.Web.Base.ViewModels;

namespace HomeMyDay.Web.Base.Managers
{
	public interface IAccommodationManager
	{
		Task<PaginatedList<Accommodation>> GetAccommodationPaginatedList(int? page, int? pageSize);

		IEnumerable<Accommodation> GetAccommodations();

		Task Delete(long id);

		Accommodation GetAccommodation(long id);

		AccommodationViewModel GetAccommodationViewModel(long id);

		IEnumerable<Accommodation> GetRecommendedAccommodations();

		Task Save(Accommodation accommodation);

		IEnumerable<Accommodation> Search(string location, DateTime departure, DateTime returnDate, int amountOfGuests);

		string GetClientApiKey();
	}
}
