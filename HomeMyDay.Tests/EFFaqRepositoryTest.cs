using System;
using HomeMyDay.Database;
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
		    
	    }

	    [Fact]
	    public void TestFaqCmsEmptyList()
	    {
		    
	    }

	    [Fact]
	    public void TestFaqCmsFilledList()
	    {
		    
	    }

	    [Fact]
	    public void TestFaqCmsPageArgumentOutOfRangeException()
	    {
		    
	    }

	    [Fact]
	    public void TestFaqCmsPageSizeArgumentOutOfRangeException()
	    {
		    
	    }
    }
}
