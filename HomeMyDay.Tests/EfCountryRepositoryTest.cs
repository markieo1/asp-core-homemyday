using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Moq;
using HomeMyDay.Database;
using Microsoft.EntityFrameworkCore;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;

namespace HomeMyDay.Tests
{
    public class EfCountryRepositoryTest
    {
		[Fact]
		public void TestCountries()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Countries.Add(new Country() {
				Id = 1,
				CountryCode = "AZE",
				Name = "Azerbaijan"
			});
			context.Countries.Add(new Country()
			{
				Id = 2,
				CountryCode = "USA",
				Name = "United States"
			});

			context.SaveChanges();

			ICountryRepository repository = new EFCountryRepository(context);

			Assert.Equal(2, repository.Countries.Count());
		}

		[Fact]
		public void TestNoCountries()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			ICountryRepository repository = new EFCountryRepository(context);

			Assert.Equal(0, repository.Countries.Count());
		}
	}
}
