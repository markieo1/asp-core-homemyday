using HomeMyDay.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Core.Models;
using HomeMyDay.Core;
using Microsoft.EntityFrameworkCore;
using HomeMyDay.Core.Repository;

namespace HomeMyDay.Infrastructure.Repository
{
	public class EFPageRepository : IPageRepository
	{
		private readonly HomeMyDayDbContext _context;

		public EFPageRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Page> Pages => _context.Page;

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
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(id));
			}

			Page page = _context.Page.LastOrDefault(r => r.Id == id);

			if (page == null)
			{
				throw new KeyNotFoundException($"Page with ID: {id} is not found");
			}

			return page;
		}

		public async Task EditPage(long id, Page page)
		{
			var db = _context.Page.Any(r => r.Id == id);

			if (page == null)
			{
				throw new ArgumentNullException(nameof(page));
			}

			if (db)
			{
				var db_page = _context.Page.LastOrDefault(r => r.Id == id);

				db_page.Title = page.Title;
				db_page.Content = page.Content;
			}
			await _context.SaveChangesAsync();
		}

		public void AddPage(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException(nameof(page));
			}
				_context.Page.Add(page);

			_context.SaveChanges();
		}

		public async Task DeletePage(long id)
		{
			Page page = _context.Page.FirstOrDefault(p => p.Id == id);

			if (page == null)
			{
				throw new KeyNotFoundException($"Page with id {id} not found");
			}

			_context.Page.Remove(page);

			await _context.SaveChangesAsync();
		}
	}
}
