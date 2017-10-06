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

		    var target = new FaqCmsController(repository);	  
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

		    var target = new FaqCmsController(repository);
		    var result = target.Index(1, 10).Result as ViewResult;
		    var model = result.Model as IEnumerable<FaqCategory>;

		    Assert.NotNull(model);
		    Assert.True(model.Count() == 3);
		}

	    [Fact]
	    public void TestFaqPageArgumentOutOfRangeException()
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

		    var target = new FaqCmsController(repository);

		    Assert.Throws<AggregateException>(() => target.Index(0, 10).Result);	
		}

	    [Fact]
	    public void TestFaqPageSizeArgumentOutOfRangeException()
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

		    var target = new FaqCmsController(repository);

		    Assert.Throws<AggregateException>(() => target.Index(1, 0).Result);
	    }

		[Fact]
		public void TestDeleteFaqCategory()
		{
			FaqCategory cat = new FaqCategory { Id = 1, CategoryName = "Test" };

			Mock<IFaqRepository> mock = new Mock<IFaqRepository>();
			mock.Setup(m => m.Categories).Returns(new FaqCategory[] {
			new FaqCategory {Id = 1, CategoryName = "Test2"},cat, new FaqCategory {Id = 3, CategoryName = "Test33"},
			});

			FaqCmsController target = new FaqCmsController(mock.Object);

			target.DeleteCategory(cat.Id);

			mock.Verify(m => m.DeleteFaqCategory(cat.Id));
		}

		[Fact]
		public void TestEditCategory()
		{
			Mock<IFaqRepository> mock = new Mock<IFaqRepository>();

			FaqCmsController target = new FaqCmsController(mock.Object);

			FaqCategory cat = new FaqCategory { Id = 1, CategoryName = "Test" };

			// Act - try to save
			IActionResult result = target.EditCategory(cat);
			// Assert - check that the repository was called
			mock.Verify(m => m.SaveFaqCategory(cat));

			// Assert - check the result type is a redirection
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
		}
	}
}
