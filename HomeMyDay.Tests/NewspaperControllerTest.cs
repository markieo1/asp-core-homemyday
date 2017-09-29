using System;
using HomeMyDay.Controllers;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
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
			INewspaperRepository repository = new EFNewspaperRepository(context);
			var target = new NewspaperController(repository);
			var newspaperViewModel = new NewspaperViewModel()
			{
				Email = "test@avans.nl"
			};
			var result = target.Index(newspaperViewModel);
									 
			Assert.NotNull(result);
			Assert.NotNull(newspaperViewModel);
			Assert.NotNull(result.ViewName);
			Assert.Equal("Result", result.ViewName);
		}

		[Fact]
		public void TestErrorViewOnSubscription()
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
			var result = target.Index(newspaperViewModel);
									 
			Assert.NotNull(result);
			Assert.NotNull(newspaperViewModel);
			Assert.Null(result.ViewName);
			Assert.NotNull(result.ViewData);
			Assert.True(result.ViewData.ModelState.ErrorCount > 0);
		}  

		[Fact]
		public void TestArgumentExceptionWhiteSpaceOnSubscription()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);
			var repository = new EFNewspaperRepository(context);
			var target = new NewspaperController(repository);

			Assert.Throws<ArgumentNullException>(() => target.Index(new NewspaperViewModel() { Email = "" }));
		}

		[Fact]
		public void TestArgumentExceptionNullOnSubscription()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);
			var repository = new EFNewspaperRepository(context);
			var target = new NewspaperController(repository);

			Assert.Throws<ArgumentNullException>(() => target.Index(new NewspaperViewModel() { Email = null }));
		}
	}
}
