using HomeMyDay.Components;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace HomeMyDay.Tests
{
	public class EFPageRepositoryTest
	{
		
		[Fact]
		public void TestGetSurprise()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Page.AddRange(
				new Page() { Page_Name= "TheSurprise", Title = "Surprise", Content = "Hallo" },
				new Page() { Page_Name = "TheSurprise", Title = "LastSurprise", Content = "Hallo" }
				);

			context.SaveChanges();

			IPageRepository repository = new EFPageRepository(context);

			Assert.Equal("Surprise", repository.GetPage(1).Title);
			Assert.Equal("LastSurprise", repository.GetPage(2).Title);
		}

		[Fact]
		public void TestEditSurprisePage()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Page.Add(new Page() { Id =1, Page_Name = "TheSurprise", Title = "LastSurprise", Content = "Hallo" });

			context.SaveChanges();

			Page page = new Page() {Title = "NewSurprise", Content = "NewContent" };

			IPageRepository repository = new EFPageRepository(context);

			repository.EditPage(1, page);

			Assert.Equal("NewSurprise", repository.GetPage(1).Title);
			Assert.Equal("NewContent", repository.GetPage(1).Content);
		}

		[Fact]
		public void TestGetIdBelowZeroAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IPageRepository repository = new EFPageRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.GetPage(0));
		}

		[Fact]
		public void TestGetIdNotExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Page.Add(new Page()
			{
				Page_Name = "Test0",
				Content = "Test1",
				Title = "Test2",
				Id = 1
			});

			context.SaveChanges();

			IPageRepository repository = new EFPageRepository(context);

			Assert.Throws<KeyNotFoundException>(() => repository.GetPage(2));
		}

		[Fact]
		public void TestGetIdExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Page.Add(new Page()
			{
				Page_Name = "Test0",
				Content = "Test1",
				Title = "Test2",
				Id = 1
			});

			context.SaveChanges();

			IPageRepository repository = new EFPageRepository(context);

			Page page = repository.GetPage(1);

			Assert.NotNull(page);
			Assert.Equal("Test2", page.Title);
		}
	}
}
