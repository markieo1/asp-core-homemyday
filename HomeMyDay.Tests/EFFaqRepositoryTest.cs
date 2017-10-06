using System;
using System.Linq;
using HomeMyDay.Database;
using HomeMyDay.Helpers;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeMyDay.Tests
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
				new FaqCategory() { CategoryName = "TestA"},
				new FaqCategory() { CategoryName = "TestB"},
				new FaqCategory() { CategoryName = "TestC"}
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

		    var faqCategories = repository.List();
			Assert.True(!faqCategories.Result.Any());
	    }

	    [Fact]
	    public void TestFaqFilledList()
	    {
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
		    optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
		    HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

		    context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "TestA"},
				new FaqCategory() { CategoryName = "TestB"}
		    );
		    context.SaveChanges();

			IFaqRepository repository = new EFFaqRepository(context);	
		    var faqCategories = repository.List();

			Assert.NotNull(faqCategories);
			Assert.True(faqCategories.Result.Count() == 2);
		}

	    [Fact]
	    public void TestFaqPageArgumentOutOfRangeException()
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

		    Assert.Throws<ArgumentOutOfRangeException>(() => repository.List(0).Result);
	    }

	    [Fact]
	    public void TestFaqPageSizeArgumentOutOfRangeException()
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

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.List(1, 0).Result);
		}
	}
}
