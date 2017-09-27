using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HomeMyDay.Tests
{
	public class EFAccommodationRepositoryTest
	{
		[Fact]
		public void TestGetIdBelowZeroAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.GetAccommodation(0));
		}

		[Fact]
		public void TestGetIdNotExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);

			context.Accommodations.Add(new Accommodation()
			{
				Location = "Amsterdam",
				MaxPersons = 4,
				Id = 1
			});

			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Assert.Throws<KeyNotFoundException>(() => repository.GetAccommodation(2));
		}

		[Fact]
		public void TestGetIdExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);

			context.Accommodations.Add(new Accommodation()
			{
				Location = "Amsterdam",
				MaxPersons = 4,
				Id = 1
			});

			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Accommodation accommodation = repository.GetAccommodation(1);

			Assert.NotNull(accommodation);
			Assert.Equal("Amsterdam", accommodation.Location);
		}
	}
}
