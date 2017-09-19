using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HomeMyDay.Tests
{
	public class EFHolidayRepositoryTest
	{
		[Fact]
		public void TestSearchEmptyLocation()
		{
			var optionsBuilder = new DbContextOptionsBuilder();

			optionsBuilder.UseInMemoryDatabase("HolidayDatabase");
			object context = null;
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentNullException>(() => repository.Search("", DateTime.Now, DateTime.Now, 1));
		}

		[Fact]
		public void TestSearchEmptyArrivalDate()
		{
			var optionsBuilder = new DbContextOptionsBuilder();

			optionsBuilder.UseInMemoryDatabase("HolidayDatabase");
			object context = null;
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", new DateTime(), DateTime.Now, 1));
		}

		[Fact]
		public void TestSearchEmptyLeaveDate()
		{
			var optionsBuilder = new DbContextOptionsBuilder();

			optionsBuilder.UseInMemoryDatabase("HolidayDatabase");
			object context = null;
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", DateTime.Now, new DateTime(), 1));
		}

		[Fact]
		public void TestSearchEmptyGuests()
		{
			var optionsBuilder = new DbContextOptionsBuilder();

			optionsBuilder.UseInMemoryDatabase("HolidayDatabase");
			object context = null;
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentNullException>(() => repository.Search("Amsterdam", DateTime.Now, DateTime.Now, 0));
		}
	}
}
