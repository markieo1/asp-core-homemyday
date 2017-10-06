﻿using HomeMyDay.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Models;
using HomeMyDay.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HomeMyDay.Repository.Implementation
{
	public class EFPageRepository : IPageRepository
	{
		private readonly HomeMyDayDbContext _context;

		public EFPageRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}

		public Task<PaginatedList<Page>> List(int page = 1, int pageSize = 10)
		{
			if (page < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(page));
			}

			if (pageSize < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(pageSize));
			}

			IQueryable<Page> _page = _context.Page.OrderBy(x => x.Id).AsNoTracking();

			return PaginatedList<Page>.CreateAsync(_page, page, pageSize);
		}

		public Page GetPage(long id)
		{
			return _context.Page.Where(r => r.Id == id).LastOrDefault();
		}

		public void EditPage(long id, Page page)
		{
			var db = _context.Page.Any(r=>r.Id == id);

			if (db)
			{
				var db_page = _context.Page.Where(r => r.Id == id).LastOrDefault();

				db_page.Title = page.Title;
				db_page.Content = page.Content;

				_context.SaveChanges();
			}
		}
	}
}