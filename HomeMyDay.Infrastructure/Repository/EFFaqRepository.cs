using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using HomeMyDay.Core.Repository;

namespace HomeMyDay.Infrastructure.Repository
{
	public class EFFaqRepository : IFaqRepository
	{
		private readonly HomeMyDayDbContext _context;

		public EFFaqRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}

		public FaqCategory GetCategory(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(id));
			}

			FaqCategory category = _context.FaqCategory
				.FirstOrDefault(a => a.Id == id);

			if (category == null)
			{
				throw new KeyNotFoundException($"Category with ID: {id} is not found");
			}

			return category;
		}

		public FaqQuestion GetQuestion(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(id));
			}

			FaqQuestion question = _context.FaqQuestions
				.FirstOrDefault(a => a.Id == id);

			if (question == null)
			{
				throw new KeyNotFoundException($"Question with ID: {id} is not found");
			}

			return question;
		}

		public async Task DeleteCategory(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(id));
			}

			FaqCategory category = await _context.FaqCategory
				.SingleOrDefaultAsync(a => a.Id == id);

			if (category == null)
			{
				throw new ArgumentNullException(nameof(id), $"Category with ID: {id} not found!");
			}

			_context.FaqCategory.Remove(category);

			await _context.SaveChangesAsync();
		}

		public async Task DeleteQuestion(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(id));
			}

			FaqQuestion question = await _context.FaqQuestions
				.SingleOrDefaultAsync(a => a.Id == id);

			if (question == null)
			{
				throw new ArgumentNullException(nameof(id), $"Question with ID: {id} not found!");
			}

			_context.FaqQuestions.Remove(question);

			await _context.SaveChangesAsync();
		}

		public async Task SaveCategory(FaqCategory category)
		{
			if (category == null)
			{
				throw new ArgumentNullException(nameof(category));
			}

			if (category.Id <= 0)
			{
				// We are creating a new one
				// Only need to adjust the id to be 0 and save it in the db.
				await _context.FaqCategory.AddAsync(category);
			}
			else
			{
				// Get the tracked accommodation using the ID
				EntityEntry<FaqCategory> entityEntry = _context.Entry(category);
				entityEntry.State = EntityState.Modified;
			}

			await _context.SaveChangesAsync();
		}

		public async Task SaveQuestion(FaqQuestion faqQuestion)
		{
			if (faqQuestion == null)
			{
				throw new ArgumentNullException(nameof(faqQuestion));
			}

			if (faqQuestion.Id <= 0)
			{
				// We are creating a new one
				// Only need to adjust the id to be 0 and save it in the db.
				await _context.FaqQuestions.AddAsync(faqQuestion);
			}
			else
			{
				// Get the tracked faqQuestion using the ID
				EntityEntry<FaqQuestion> entityEntry = _context.Entry(faqQuestion);
				entityEntry.State = EntityState.Modified;
			}

			await _context.SaveChangesAsync();
		}

		public IEnumerable<FaqCategory> GetCategoriesAndQuestions()
		{
			return _context.FaqCategory.Include(nameof(FaqCategory.Questions));
		}

		public IEnumerable<FaqQuestion> GetQuestions(long id)
		{
			return _context.FaqCategory.FirstOrDefault(c => c.Id == id)?.Questions;
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

			var faqQuestions = _context.FaqCategory
				.Where(x => x.Id == categoryId)
				.First()
				.Questions
				.AsQueryable();

			return PaginatedList<FaqQuestion>.CreateAsync(faqQuestions, page, pageSize);
		}
	}
}