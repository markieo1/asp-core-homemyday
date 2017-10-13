using System.Collections.Generic;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Base.Managers
{
	public interface ICountryManager
    {
	    IEnumerable<Country> GetCountries();

	    Country GetCountry(long id);
    }
}
