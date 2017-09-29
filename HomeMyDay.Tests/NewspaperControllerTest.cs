using System;
using HomeMyDay.Controllers;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository.Implementation;
using HomeMyDay.ViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeMyDay.Tests
{
	public class NewspaperControllerTest
    {
	    [Fact]
	    public void TestResultViewOnSubscription()
	    {
		    var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
		    optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);
			var repository = new EFNewspaperRepository(context);
		    var target = new NewspaperController(repository);  
		    var newspaperViewModel = new NewspaperViewModel()
		    {
				Email = "test@avans.nl"
		    };	
		    var result = target.Subscribe(newspaperViewModel);	

			Assert.NotNull(target);
			Assert.NotNull(result);
			Assert.NotNull(newspaperViewModel);
			Assert.NotNull(result.ViewName);
			Assert.Equal("Result", result.ViewName);
	    }

	    [Fact]
	    public void TestErrorOnSubscription()
	    {
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
		    optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
		    var context = new HomeMyDayDbContext(optionsBuilder.Options);

		    context.Newspapers.Add(new Newspaper()
		    {
			   Email = "test@avans.nl"
		    });

		    context.SaveChanges();

			var repository = new EFNewspaperRepository(context);
		    var target = new NewspaperController(repository);  
		    var newspaperViewModel = new NewspaperViewModel()
		    {
			    Email = "test@avans.nl"
		    }; 
		    var result = target.Subscribe(newspaperViewModel);

		    Assert.NotNull(target);
		    Assert.NotNull(result);
			Assert.NotNull(newspaperViewModel);
		    Assert.Null(result.ViewName);  
			Assert.NotNull(result.ViewData);
			Assert.True(result.ViewData.ModelState.ErrorCount > 0);
		}

		[Fact]
		public void TestArgumentExceptionOnSubscription()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);
			var repository = new EFNewspaperRepository(context);
			var target = new NewspaperController(repository);

			Assert.NotNull(target);	 
			Assert.Throws<ArgumentNullException>(() => target.Subscribe(new NewspaperViewModel(){ Email = ""}));
			Assert.Throws<ArgumentNullException>(() => target.Subscribe(new NewspaperViewModel(){ Email = null}));
		}
	}
}
