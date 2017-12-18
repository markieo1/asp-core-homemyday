using System.Collections.Generic;
using HomeMyDay.Core.Models;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Base.Managers
{
	public interface ICountryManager
    {
	    IEnumerable<Country> GetCountries();

	    Country GetCountry(long id);

		Task Save(Country country);

		Task Delete(long id);
    }
}
