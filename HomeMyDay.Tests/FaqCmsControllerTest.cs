using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeMyDay.Controllers.Cms;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace HomeMyDay.Tests
{
	public class FaqCmsControllerTest
	{
		[Fact]
		public void TestEmptyFaqList()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IFaqRepository repository = new EFFaqRepository(context);

			var target = new FaqController(repository);
			var result = target.Index(1, 10).Result as ViewResult;
			var model = result.Model as IEnumerable<FaqCategory>;

			Assert.NotNull(model);
			Assert.True(!model.Any());
		}

		[Fact]
		public void TestFilledFaqList()
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

			var target = new FaqController(repository);
			var result = target.Index(1, 10).Result as ViewResult;
			var model = result.Model as IEnumerable<FaqCategory>;

			Assert.NotNull(model);
			Assert.True(model.Count() == 3);
		}

		[Fact]
		public async void TestDeleteFaqCategoryCalled()
		{
			FaqCategory cat = new FaqCategory { Id = 1, CategoryName = "Test" };

			Mock<IFaqRepository> mock = new Mock<IFaqRepository>();
			mock.Setup(m => m.Categories).Returns(new FaqCategory[] {
			new FaqCategory {Id = 1, CategoryName = "Test2"},cat, new FaqCategory {Id = 3, CategoryName = "Test33"},
			});

			FaqController target = new FaqController(mock.Object);
			//try to delete
			await target.DeleteCategory(cat.Id);
			//Check if DeleteCategory is called 
			mock.Verify(m => m.DeleteCategory(cat.Id));
		}

		[Fact]
		public async void TestEditCategoryCalled()
		{
			Mock<IFaqRepository> mock = new Mock<IFaqRepository>();

			FaqController target = new FaqController(mock.Object);

			FaqCategory cat = new FaqCategory { Id = 1, CategoryName = "Test" };

			// try to save
			IActionResult result = await target.EditCategory(cat);
			//check that the repository was called
			mock.Verify(m => m.SaveCategory(cat));

			//check the result type is a redirection
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
		}

		#region "Save"

		[Fact]
		public async void TestSaveNullCategory()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "Test" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			await Assert.ThrowsAsync<ArgumentNullException>(() => repository.SaveCategory(null));
		}

		[Fact]
		public async void TestSaveNewCategory()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "Test" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			FaqCategory categoryToCreate = new FaqCategory()
			{
				CategoryName = "Test"
			};

			await repository.SaveCategory(categoryToCreate);

			// Check if the item was created
			FaqCategory foundCat= await context.FaqCategory.FirstOrDefaultAsync(x => x.CategoryName == "Test");

			Assert.NotNull(foundCat);
			Assert.Equal("Test", foundCat.CategoryName);
		}

		[Fact]
		public async void TestSaveUpdatedCategory()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			FaqCategory catToUpdate = new FaqCategory()
			{
				Id = 1,
				CategoryName = "Test",
			};

			await context.FaqCategory.AddAsync(catToUpdate);
			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			// Change some values
			catToUpdate.CategoryName = "New";

			await repository.SaveCategory(catToUpdate);

			// Check if the item was updated
			FaqCategory updatedCat = await context.FaqCategory.FindAsync((long)1);

			Assert.NotNull(updatedCat);
			Assert.Equal("New", updatedCat.CategoryName);
		}

		[Fact]
		public async void TestSaveNotExistingCategoryWithNotExistingId()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			FaqCategory ExistingCat = new FaqCategory()
			{
				Id = 1,
				CategoryName = "Test",
			};

			await context.FaqCategory.AddAsync(ExistingCat);
			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			// Change some values
			FaqCategory catToUpdate = new FaqCategory()
			{
				Id = 2,
				CategoryName = "New",
			};

			await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => repository.SaveCategory(catToUpdate));
		}

		#endregion

		#region "Delete"

		[Fact]
		public async void TestDeleteExistingCategory()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() {Id = 1, CategoryName = "Test" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			await repository.DeleteCategory(1);

			FaqCategory deletedCat = await context.FaqCategory.FindAsync((long)1);
			Assert.Null(deletedCat);
		}

		[Fact]
		public async void TestDeleteNotExistingCategory()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { Id = 1, CategoryName = "Test" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			await Assert.ThrowsAsync<ArgumentNullException>(() => repository.DeleteCategory(2));
		}

		[Fact]
		public async void TestDeleteIdBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "Test" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => repository.DeleteCategory(-10));
		}

		[Fact]
		public async void TestDeleteIdEqualsZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "Test" }
			);

			await context.SaveChangesAsync();

			IFaqRepository repository = new EFFaqRepository(context);

			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => repository.DeleteCategory(0));
		}

		#endregion
	}
}
