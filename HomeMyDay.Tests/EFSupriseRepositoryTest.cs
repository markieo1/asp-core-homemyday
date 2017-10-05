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
	public class EFSupriseRepositoryTest
	{
		
		[Fact]
		public void TestGetSuprise()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Page.AddRange(
				new Suprise() { Title = "Suprise", Content = "Hallo" },
				new Suprise() { Title = "LastSuprise", Content = "Hallo" }
				);

			context.SaveChanges();


			IPageRepository repository = new EFSupriseRepository(context);

			Assert.Equal("LastSuprise", repository.GetSuprise().Title);
		}

	}
}
