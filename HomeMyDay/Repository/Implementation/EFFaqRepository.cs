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

		public IEnumerable<FaqCategory> GetCategoriesAndQuestions()
		{
			return _context.FaqCategory.Include(nameof(FaqCategory.FaqQuestions));
		}

		public FaqCategory GetCategory(long categoryId)
		{
			if (categoryId <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(categoryId));
			}

			FaqCategory category = _context.FaqCategory
				.FirstOrDefault(a => a.Id == categoryId);

			if (category == null)
			{
				throw new KeyNotFoundException($"Category with ID: {categoryId} is not found");
			}

			return category;
		}

		public Task<PaginatedList<FaqCategory>> ListCategories(int page = 1, int pageSize = 10)
		{
			// Reset to default value
			if (pageSize <= 0)
			{
				pageSize = 10;
			}

			// We are not able to skip before the first page
			if (page <= 0)
			{
				page = 1;
			}

			var faqCategories = _context.FaqCategory.OrderBy(x => x.Id).AsNoTracking();

			return PaginatedList<FaqCategory>.CreateAsync(faqCategories, page, pageSize);
		}

		public Task<PaginatedList<FaqQuestion>> ListQuestions(long categoryId, int page = 1, int pageSize = 10)
		{
			if (categoryId <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(categoryId));
			}

			// Reset to default value
			if (pageSize <= 0)
			{
				pageSize = 10;
			}

			// We are not able to skip before the first page
			if (page <= 0)
			{
				page = 1;
			}

			var faqQuestions = _context.FaqQuestions
				.Where(x => x.CategoryId == categoryId)
				.OrderBy(x => x.Id)
				.AsNoTracking();

			return PaginatedList<FaqQuestion>.CreateAsync(faqQuestions, page, pageSize);
		}
	}
}