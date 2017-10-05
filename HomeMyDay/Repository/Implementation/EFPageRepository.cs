using HomeMyDay.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Models;

namespace HomeMyDay.Repository.Implementation
{
	public class EFPageRepository : IPageRepository
	{
		private readonly HomeMyDayDbContext _context;

		public EFPageRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}

		public Page GetSuprise()
		{
			return _context.Page.Where(r => r.Page_Id == "TheSuprise").LastOrDefault();
		}
	}
}
