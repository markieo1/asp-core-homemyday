using HomeMyDay.Components;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

namespace HomeMyDay.Tests
{
	public class EFPageRepositoryTest
	{
		
		[Fact]
		public void TestGetSuprise()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Page.AddRange(
				new Page() { Page_Id= "TheSuprise", Title = "Suprise", Content = "Hallo" },
				new Page() { Page_Id = "TheSuprise", Title = "LastSuprise", Content = "Hallo" }
				);

			context.SaveChanges();


			IPageRepository repository = new EFPageRepository(context);

			Assert.Equal("LastSuprise", repository.GetSuprise().Title);
		}

	}
}
