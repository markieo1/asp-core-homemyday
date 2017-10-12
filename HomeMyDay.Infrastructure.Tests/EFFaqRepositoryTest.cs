using System;
using System.Linq;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeMyDay.Infrastructure.Tests
{
	public class EFFaqRepositoryTest
	{
		[Fact]
		public void TestFaqEmptyRepository()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IFaqRepository repository = new EFFaqRepository(context);

			Assert.Empty(repository.GetCategoriesAndQuestions());
		}

		[Fact]
		public void TestFaqFilledRepository()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "TestA" },
				new FaqCategory() { CategoryName = "TestB" },
				new FaqCategory() { CategoryName = "TestC" }
			);
			context.SaveChanges();

			IFaqRepository repository = new EFFaqRepository(context);
			Assert.True(repository.GetCategoriesAndQuestions().Count() == 3);
		}

		[Fact]
		public void TestFaqEmptyList()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IFaqRepository repository = new EFFaqRepository(context);

			var faqCategories = repository.ListCategories();
			Assert.True(!faqCategories.Result.Any());
		}

		[Fact]
		public void TestFaqFilledList()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "TestA" },
				new FaqCategory() { CategoryName = "TestB" }
			);
			context.SaveChanges();

			IFaqRepository repository = new EFFaqRepository(context);
			var faqCategories = repository.ListCategories();

			Assert.NotNull(faqCategories);
			Assert.True(faqCategories.Result.Count() == 2);
		}

		#region "List Categories"

		[Fact]
		public async void TestCategoryListWithItemsOnePageSizeTen()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "TestA" },
				new FaqCategory() { CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqCategory> paginatedCategories = await repository.ListCategories(1, 10);

			Assert.NotNull(paginatedCategories);
			Assert.Equal(2, paginatedCategories.Count);
			Assert.Equal(1, paginatedCategories.PageIndex);
			Assert.Equal(10, paginatedCategories.PageSize);
			Assert.Equal(1, paginatedCategories.TotalPages);
			Assert.False(paginatedCategories.HasPreviousPage);
			Assert.False(paginatedCategories.HasNextPage);
		}

		[Fact]
		public async void TestCategoryListWithItemsMultiplePagesSizeOne()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "TestA" },
				new FaqCategory() { CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqCategory> paginatedCategories = await repository.ListCategories(1, 1);

			Assert.NotNull(paginatedCategories);
			Assert.Equal(1, paginatedCategories.Count);
			Assert.Equal(1, paginatedCategories.PageIndex);
			Assert.Equal(1, paginatedCategories.PageSize);
			Assert.Equal(2, paginatedCategories.TotalPages);
			Assert.False(paginatedCategories.HasPreviousPage);
			Assert.True(paginatedCategories.HasNextPage);
		}

		[Fact]
		public async void TestCategoryListWithItemsMultiplePagesSizeOneWithPreviousPage()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "TestA" },
				new FaqCategory() { CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqCategory> paginatedCategories = await repository.ListCategories(2, 1);

			Assert.NotNull(paginatedCategories);
			Assert.Equal(1, paginatedCategories.Count);
			Assert.Equal(2, paginatedCategories.PageIndex);
			Assert.Equal(1, paginatedCategories.PageSize);
			Assert.Equal(2, paginatedCategories.TotalPages);
			Assert.True(paginatedCategories.HasPreviousPage);
			Assert.False(paginatedCategories.HasNextPage);
		}

		[Fact]
		public async void TestCategoryListWithItemsPageBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "TestA" },
				new FaqCategory() { CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqCategory> paginatedCategories = await repository.ListCategories(-5, 1);

			Assert.NotNull(paginatedCategories);
			Assert.Equal(1, paginatedCategories.Count);
			Assert.Equal(1, paginatedCategories.PageIndex);
			Assert.Equal(1, paginatedCategories.PageSize);
			Assert.Equal(2, paginatedCategories.TotalPages);
			Assert.False(paginatedCategories.HasPreviousPage);
			Assert.True(paginatedCategories.HasNextPage);
		}

		[Fact]
		public async void TestCategoryListWithItemsPageSizeBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "TestA" },
				new FaqCategory() { CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqCategory> paginatedCategories = await repository.ListCategories(1, -10);

			Assert.NotNull(paginatedCategories);
			Assert.Equal(2, paginatedCategories.Count);
			Assert.Equal(1, paginatedCategories.PageIndex);
			Assert.Equal(10, paginatedCategories.PageSize);
			Assert.Equal(1, paginatedCategories.TotalPages);
			Assert.False(paginatedCategories.HasPreviousPage);
			Assert.False(paginatedCategories.HasNextPage);
		}

		[Fact]
		public async void TestCategoryListWithItemsPageBelowZeroPageSizeBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "TestA" },
				new FaqCategory() { CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqCategory> paginatedCategories = await repository.ListCategories(-8, -10);

			Assert.NotNull(paginatedCategories);
			Assert.Equal(2, paginatedCategories.Count);
			Assert.Equal(1, paginatedCategories.PageIndex);
			Assert.Equal(10, paginatedCategories.PageSize);
			Assert.Equal(1, paginatedCategories.TotalPages);
			Assert.False(paginatedCategories.HasPreviousPage);
			Assert.False(paginatedCategories.HasNextPage);
		}

		[Fact]
		public async void TestEmptyListPageOnePageSizeTen()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqCategory> paginatedCategories = await repository.ListCategories(1, 10);

			Assert.NotNull(paginatedCategories);
			Assert.Equal(0, paginatedCategories.Count);
			Assert.Equal(1, paginatedCategories.PageIndex);
			Assert.Equal(10, paginatedCategories.PageSize);
			Assert.Equal(1, paginatedCategories.TotalPages);
			Assert.False(paginatedCategories.HasPreviousPage);
			Assert.False(paginatedCategories.HasNextPage);
		}

		[Fact]
		public async void TestEmptyListPageBelowZeroPageSizeTen()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqCategory> paginatedCategories = await repository.ListCategories(-5, 10);

			Assert.NotNull(paginatedCategories);
			Assert.Equal(0, paginatedCategories.Count);
			Assert.Equal(1, paginatedCategories.PageIndex);
			Assert.Equal(10, paginatedCategories.PageSize);
			Assert.Equal(1, paginatedCategories.TotalPages);
			Assert.False(paginatedCategories.HasPreviousPage);
			Assert.False(paginatedCategories.HasNextPage);
		}

		[Fact]
		public async void TestEmptyListPageBelowZeroPageSizeBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqCategory> paginatedCategories = await repository.ListCategories(-5, -10);

			Assert.NotNull(paginatedCategories);
			Assert.Equal(0, paginatedCategories.Count);
			Assert.Equal(1, paginatedCategories.PageIndex);
			Assert.Equal(10, paginatedCategories.PageSize);
			Assert.Equal(1, paginatedCategories.TotalPages);
			Assert.False(paginatedCategories.HasPreviousPage);
			Assert.False(paginatedCategories.HasNextPage);
		}
		#endregion

		#region "List Questions"
		[Fact]
		public async void TestQuestionListWithIdZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory()
				{
					Id = 1,
					CategoryName = "TestA",
					FaqQuestions = new System.Collections.Generic.List<FaqQuestion>()
				{
					new FaqQuestion()
					{
						Question ="Question 1",
						Answer = "Answer 1"
					},
					new FaqQuestion()
					{
						Question =   "Question 2",
						Answer = "Answer 2"
					}
				}
				},
				new FaqCategory() { CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => repository.ListQuestions(0, 1, 10));
		}

		[Fact]
		public async void TestQuestionListWithIdBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory()
				{
					Id = 1,
					CategoryName = "TestA",
					FaqQuestions = new System.Collections.Generic.List<FaqQuestion>()
				{
					new FaqQuestion()
					{
						Question ="Question 1",
						Answer = "Answer 1"
					},
					new FaqQuestion()
					{
						Question =   "Question 2",
						Answer = "Answer 2"
					}
				}
				},
				new FaqCategory() { CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => repository.ListQuestions(-5, 1, 10));
		}

		[Fact]
		public async void TestQuestionListWithItemsOnePageSizeTen()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory()
				{
					Id = 1,
					CategoryName = "TestA",
					FaqQuestions = new System.Collections.Generic.List<FaqQuestion>()
				{
					new FaqQuestion()
					{
						Question ="Question 1",
						Answer = "Answer 1"
					},
					new FaqQuestion()
					{
						Question =   "Question 2",
						Answer = "Answer 2"
					}
				}
				},
				new FaqCategory() { CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqQuestion> paginatedQuestions = await repository.ListQuestions(1, 1, 10);

			Assert.NotNull(paginatedQuestions);
			Assert.Equal(2, paginatedQuestions.Count);
			Assert.Equal(1, paginatedQuestions.PageIndex);
			Assert.Equal(10, paginatedQuestions.PageSize);
			Assert.Equal(1, paginatedQuestions.TotalPages);
			Assert.False(paginatedQuestions.HasPreviousPage);
			Assert.False(paginatedQuestions.HasNextPage);
		}

		[Fact]
		public async void TestQuestionListWithItemsMultiplePagesSizeOne()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory()
				{
					Id = 1,
					CategoryName = "TestA",
					FaqQuestions = new System.Collections.Generic.List<FaqQuestion>()
				{
					new FaqQuestion()
					{
						Question ="Question 1",
						Answer = "Answer 1"
					},
					new FaqQuestion()
					{
						Question =   "Question 2",
						Answer = "Answer 2"
					}
				}
				},
				new FaqCategory() { CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqQuestion> paginatedQuestions = await repository.ListQuestions(1, 1, 1);

			Assert.NotNull(paginatedQuestions);
			Assert.Equal(1, paginatedQuestions.Count);
			Assert.Equal(1, paginatedQuestions.PageIndex);
			Assert.Equal(1, paginatedQuestions.PageSize);
			Assert.Equal(2, paginatedQuestions.TotalPages);
			Assert.False(paginatedQuestions.HasPreviousPage);
			Assert.True(paginatedQuestions.HasNextPage);
		}

		[Fact]
		public async void TestQuestionListWithItemsMultiplePagesSizeOneWithPreviousPage()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory()
				{
					Id = 1,
					CategoryName = "TestA",
					FaqQuestions = new System.Collections.Generic.List<FaqQuestion>()
				{
					new FaqQuestion()
					{
						Question ="Question 1",
						Answer = "Answer 1"
					},
					new FaqQuestion()
					{
						Question =   "Question 2",
						Answer = "Answer 2"
					}
				}
				},
				new FaqCategory() { Id = 2, CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqQuestion> paginatedQuestions = await repository.ListQuestions(1, 2, 1);

			Assert.NotNull(paginatedQuestions);
			Assert.Equal(1, paginatedQuestions.Count);
			Assert.Equal(2, paginatedQuestions.PageIndex);
			Assert.Equal(1, paginatedQuestions.PageSize);
			Assert.Equal(2, paginatedQuestions.TotalPages);
			Assert.True(paginatedQuestions.HasPreviousPage);
			Assert.False(paginatedQuestions.HasNextPage);
		}

		[Fact]
		public async void TestQuestionListWithItemsPageBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory()
				{
					Id = 1,
					CategoryName = "TestA",
					FaqQuestions = new System.Collections.Generic.List<FaqQuestion>()
				{
					new FaqQuestion()
					{
						Question ="Question 1",
						Answer = "Answer 1"
					},
					new FaqQuestion()
					{
						Question =   "Question 2",
						Answer = "Answer 2"
					}
				}
				},
				new FaqCategory() { CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqQuestion> paginatedQuestions = await repository.ListQuestions(1, -5, 1);

			Assert.NotNull(paginatedQuestions);
			Assert.Equal(1, paginatedQuestions.Count);
			Assert.Equal(1, paginatedQuestions.PageIndex);
			Assert.Equal(1, paginatedQuestions.PageSize);
			Assert.Equal(2, paginatedQuestions.TotalPages);
			Assert.False(paginatedQuestions.HasPreviousPage);
			Assert.True(paginatedQuestions.HasNextPage);
		}

		[Fact]
		public async void TestQuestionListWithItemsPageSizeBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory()
				{
					Id = 1,
					CategoryName = "TestA",
					FaqQuestions = new System.Collections.Generic.List<FaqQuestion>()
				{
					new FaqQuestion()
					{
						Question ="Question 1",
						Answer = "Answer 1"
					},
					new FaqQuestion()
					{
						Question =   "Question 2",
						Answer = "Answer 2"
					}
				}
				},
				new FaqCategory() { CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqQuestion> paginatedQuestions = await repository.ListQuestions(1, 1, -10);

			Assert.NotNull(paginatedQuestions);
			Assert.Equal(2, paginatedQuestions.Count);
			Assert.Equal(1, paginatedQuestions.PageIndex);
			Assert.Equal(10, paginatedQuestions.PageSize);
			Assert.Equal(1, paginatedQuestions.TotalPages);
			Assert.False(paginatedQuestions.HasPreviousPage);
			Assert.False(paginatedQuestions.HasNextPage);
		}

		[Fact]
		public async void TestQuestionListWithItemsPageBelowZeroPageSizeBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory()
				{			
					CategoryName = "TestA",
					FaqQuestions = new System.Collections.Generic.List<FaqQuestion>()
				{
					new FaqQuestion()
					{
						Question ="Question 1",
						Answer = "Answer 1"
					},
					new FaqQuestion()
					{
						Question =   "Question 2",
						Answer = "Answer 2"
					}
				}
				},
				new FaqCategory() { CategoryName = "TestB" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			PaginatedList<FaqQuestion> paginatedQuestions = await repository.ListQuestions(1, -8, -10);

			Assert.NotNull(paginatedQuestions);
			Assert.Equal(2, paginatedQuestions.Count);
			Assert.Equal(1, paginatedQuestions.PageIndex);
			Assert.Equal(10, paginatedQuestions.PageSize);
			Assert.Equal(1, paginatedQuestions.TotalPages);
			Assert.False(paginatedQuestions.HasPreviousPage);
			Assert.False(paginatedQuestions.HasNextPage);
		}
		#endregion
	}
}
