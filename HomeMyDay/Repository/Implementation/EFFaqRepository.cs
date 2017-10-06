using HomeMyDay.Database;
using HomeMyDay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Helpers;

namespace HomeMyDay.Repository.Implementation
{
	public class EFFaqRepository : IFaqRepository
	{
		private readonly HomeMyDayDbContext _context;

		public EFFaqRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}

		public IEnumerable<FaqCategory> Categories => _context.FaqCategory;

		public FaqCategory DeleteFaqCategory(long id)
		{

			var cat = _context.FaqCategory.FirstOrDefault(c => c.Id == id);

			if (cat != null)
			{
				_context.FaqCategory.Remove(cat);
				_context.SaveChanges();
			}

			return cat;
		}

		public void SaveFaqCategory(FaqCategory category)
		{
			if (category.Id == 0)
			{
				_context.FaqCategory.Add(category);
			}
			else
			{
				FaqCategory cat = _context.FaqCategory.FirstOrDefault(c => c.Id == category.Id);
				if (cat != null)
				{
					cat.CategoryName = category.CategoryName;
				}
			}
			_context.SaveChanges();
		}

		public IEnumerable<FaqCategory> GetCategoriesAndQuestions()
		{
			return _context.FaqCategory.Include(nameof(FaqCategory.FaqQuestions));
		}

		public Task<PaginatedList<FaqCategory>> List(int page = 1, int pageSize = 10)
		{
			if (page < 1)
			{
				throw new ArgumentOutOfRangeException();
			}

			if (pageSize < 1)
			{
				throw new ArgumentOutOfRangeException();
			}

			var faqCategories = _context.FaqCategory.OrderBy(x => x.Id).AsNoTracking();

			return PaginatedList<FaqCategory>.CreateAsync(faqCategories, page, pageSize);
		}
	}
}