using HomeMyDay.Database;
using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Repository.Implementation
{
    public class EFCountryRepository : ICountryRepository
    {
		private readonly HomeMyDayDbContext _context;

		public IEnumerable<Country> Countries => _context.Countries;

		public EFCountryRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}
	}
}
