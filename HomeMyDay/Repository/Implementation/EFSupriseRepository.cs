using HomeMyDay.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Models;

namespace HomeMyDay.Repository.Implementation
{
    public class EFSupriseRepository : ISupriseRepository
    {
		private readonly HomeMyDayDbContext _context;

		public EFSupriseRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}

		public virtual Suprise GetLastSuprise()
		{
			return _context.Suprise.LastOrDefault();
		}
	}
}
