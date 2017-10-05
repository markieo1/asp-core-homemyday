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

		public void EditPage(string pageid, Page page)
		{
			var db = _context.Page.Any(r=>r.Page_Id == pageid);

			if (db)
			{
				var db_page = _context.Page.Where(r => r.Page_Id == pageid).LastOrDefault();

				db_page.Title = page.Title;
				db_page.Content = page.Content;

				_context.SaveChanges();
			}
		}
	}
}
