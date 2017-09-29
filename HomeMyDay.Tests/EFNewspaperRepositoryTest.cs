using System;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeMyDay.Tests
{
	public class EFNewspaperRepositoryTest
	{
		[Fact]
		public void TestInsertSubscription()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);
			var repository = new EFNewspaperRepository(context);
			string email = "test@avans.nl";
									  	  
			Assert.True(repository.Subscribe(email));
		}

		[Fact]
		public void TestInsertWhiteSpaceSubscription()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);
			var repository = new EFNewspaperRepository(context);
			string email = "  test@avans.nl  ";

			Assert.True(repository.Subscribe(email));
		}

		[Fact]
		public void TestInsertDuplicateSubscription()
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
			string email = "test@avans.nl";
										
			Assert.False(repository.Subscribe(email));
		}	

		[Fact]
		public void TestArgumentExceptionEmptyStringOnSubscription()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);
			var repository = new EFNewspaperRepository(context);
									   
			Assert.Throws<ArgumentNullException>(() => repository.Subscribe(""));	
		}

		[Fact]
		public void TestArgumentExceptionWhiteSpaceOnSubscription()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);
			var repository = new EFNewspaperRepository(context);

			Assert.Throws<ArgumentNullException>(() => repository.Subscribe("  "));
		}

		[Fact]
		public void TestArgumentExceptionNullOnSubscription()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new HomeMyDayDbContext(optionsBuilder.Options);
			var repository = new EFNewspaperRepository(context);
																				   
			Assert.Throws<ArgumentNullException>(() => repository.Subscribe(null));
		}
	}
}
