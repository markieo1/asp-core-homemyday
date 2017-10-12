using HomeMyDay.Web.Home.Database;
using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Home.Repository.Implementation
{
    public class EFCountryRepository : ICountryRepository
    {
		private readonly HomeMyDayDbContext _context;

		public IEnumerable<Country> Countries => _context.Countries;

		public EFCountryRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}

		public Country GetCountry(long id)
		{
			var country = _context.Countries.First(c => c.Id == id);

			return country;
		}
	}
}
