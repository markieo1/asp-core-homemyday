﻿using System;
using System.Linq;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeMyDay.Infrastructure.Tests
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

			Assert.Empty(repository.Countries);
		}
	}
}
