using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HomeMyDay.Tests
{
	public class EFHolidayRepositoryTest
	{
		[Fact]
		public void TestSearchEmptyLocation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase("HolidayDatabase");
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentNullException>(() => repository.Search("", DateTime.Now, DateTime.Now, 1));
		}

		[Fact]
		public void TestSearchEmptyArrivalDate()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase("HolidayDatabase");
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", new DateTime(), DateTime.Now, 1));
		}

		[Fact]
		public void TestSearchEmptyLeaveDate()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase("HolidayDatabase");
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", DateTime.Now, new DateTime(), 1));
		}

		[Fact]
		public void TestSearchEmptyGuests()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase("HolidayDatabase");
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentNullException>(() => repository.Search("Amsterdam", DateTime.Now, DateTime.Now, 0));
		}

		[Fact]
		public void TestSearchReturnAfterDeparture()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase("HolidayDatabase");
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", new DateTime(2017, 10, 12), new DateTime(2017, 10, 11), 4));
		}

		[Fact]
		public void TestSearchDepartureAndReturnExistingHoliday()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase("HolidayDatabase");
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);

			context.Holidays.Add(new Holiday()
			{
				DepartureDate = new DateTime(2017, 10, 12),
				ReturnDate = new DateTime(2017, 10, 22),
				NrPersons = 4,
				Accommodation = new Models.Accommodation()
				{
					Name = "Amsterdam"
				}
			});

			context.SaveChanges();

			IHolidayRepository repository = new EFHolidayRepository(context);

			IEnumerable<Holiday> searchResults = repository.Search("Amsterdam", new DateTime(2017, 10, 13), new DateTime(2017, 10, 19), 4);

			Assert.NotEmpty(searchResults);

			Holiday firstResult = searchResults.First();
			Assert.NotNull(firstResult);
			Assert.True(firstResult.DepartureDate == new DateTime(2017, 10, 12));
			Assert.True(firstResult.ReturnDate == new DateTime(2017, 10, 22));
			Assert.True(firstResult.NrPersons == 4);
			Assert.True(firstResult.Accommodation.Name == "Amsterdam");
		}
	}
}
