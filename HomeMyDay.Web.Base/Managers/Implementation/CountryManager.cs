using System.Collections.Generic;
using System.Linq;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Base.Managers.Implementation
{
	public class CountryManager : ICountryManager
    {
	    private readonly ICountryRepository _countryRepository;

	    public CountryManager(ICountryRepository countryRepository)
	    {
		    _countryRepository = countryRepository;	 
	    }

	    public IEnumerable<Country> GetCountries()
	    {
		    return _countryRepository.Countries.OrderBy(c => c.Name);
	    }

	    public Country GetCountry(long id)
	    {
		    return _countryRepository.GetCountry(id);
	    }

		public Task Save(Country country)
		{
			return _countryRepository.Save(country);
		}

		public Task Delete(long id)
		{
			return _countryRepository.Delete(id);
		}
    }
}
