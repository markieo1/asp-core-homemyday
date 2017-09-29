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
	    public void TestSubscribe()
	    {
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
		    optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
		    var context = new HomeMyDayDbContext(optionsBuilder.Options);
		    var repository = new EFNewspaperRepository(context);
		    string email = "test@avans.nl";

			Assert.NotNull(repository);
			Assert.True(repository.Subscribe(email));
	    }

	    [Fact]
	    public void TestSubscribeError()
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

			Assert.NotNull(repository);
			Assert.False(repository.Subscribe(email));
		}
	}
}
