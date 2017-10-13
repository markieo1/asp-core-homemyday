using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Core.Repository;

namespace HomeMyDay.Infrastructure.Repository
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
