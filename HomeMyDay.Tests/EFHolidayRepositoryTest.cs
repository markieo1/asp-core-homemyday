using HomeMyDay.Components;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using Microsoft.AspNetCore.Mvc.ViewComponents;
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
        public void TestHolidaysEmptyRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
            IHolidayRepository repository = new EFHolidayRepository(context);

            Assert.Empty(repository.Holidays);
        }

        [Fact]
        public void TestHolidaysTrueRecommended()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);

            context.Holidays.AddRange(
                new Holiday() { Image = "/images/holiday/image-1.jpg", Description = "Dit is een omschrijving", Recommended = false },
                new Holiday() { Image = "/images/holiday/image-2.jpg", Description = "Dit is een omschrijving", Recommended = true },
                new Holiday() { Image = "/images/holiday/image-3.jpg", Description = "Dit is een omschrijving", Recommended = false },
                new Holiday() { Image = "/images/holiday/image-4.jpg", Description = "Dit is een omschrijving", Recommended = true }
            );
            context.SaveChanges();

            IHolidayRepository repository = new EFHolidayRepository(context);

            RecommendedHoliday component = new RecommendedHoliday(repository);

            IEnumerable<Holiday> holiday = ((IEnumerable<Holiday>)(component.Invoke() as ViewViewComponentResult).ViewData.Model);

            foreach (var h in holiday)
            {
                Assert.True(h.Recommended == true);
            }
        }

        [Fact]
		public void TestSearchEmptyLocation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentNullException>(() => repository.Search("", DateTime.Now, DateTime.Now, 1));
		}

		[Fact]
		public void TestSearchEmptyArrivalDate()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", new DateTime(), DateTime.Now, 1));
		}

		[Fact]
		public void TestSearchEmptyLeaveDate()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", DateTime.Now, new DateTime(), 1));
		}

		[Fact]
		public void TestSearchEmptyGuests()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentNullException>(() => repository.Search("Amsterdam", DateTime.Now, DateTime.Now, 0));
		}

		[Fact]
		public void TestSearchReturnAfterDeparture()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);
			IHolidayRepository repository = new EFHolidayRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", new DateTime(2017, 10, 12), new DateTime(2017, 10, 11), 4));
		}

		[Fact]
		public void TestSearchDepartureAndArrivalReturnExistingHoliday()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);

			context.Holidays.Add(new Holiday()
			{
				DepartureDate = new DateTime(2017, 10, 12),
				ReturnDate = new DateTime(2017, 10, 22),
				Accommodation = new Models.Accommodation()
				{
					Name = "Amsterdam",
					MaxPersons = 4
				}
			});

			context.SaveChanges();

			IHolidayRepository repository = new EFHolidayRepository(context);

			IEnumerable<Holiday> searchResults = repository.Search("Amsterdam", new DateTime(2017, 10, 11), new DateTime(2017, 10, 23), 4);

			Assert.NotEmpty(searchResults);

			Holiday firstResult = searchResults.FirstOrDefault();
			Assert.NotNull(firstResult);
			Assert.True(firstResult.DepartureDate == new DateTime(2017, 10, 12));
			Assert.True(firstResult.ReturnDate == new DateTime(2017, 10, 22));
			Assert.True(firstResult.Accommodation.MaxPersons == 4);
			Assert.True(firstResult.Accommodation.Name == "Amsterdam");
		}

		[Fact]
		public void TestSearchDepartureAndArrivalReturnNoHoliday()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);

			context.Holidays.Add(new Holiday()
			{
				DepartureDate = new DateTime(2017, 10, 12),
				ReturnDate = new DateTime(2017, 10, 22),
				Accommodation = new Models.Accommodation()
				{
					Name = "Amsterdam",
					MaxPersons = 4
				}
			});

			context.SaveChanges();

			IHolidayRepository repository = new EFHolidayRepository(context);

			IEnumerable<Holiday> searchResults = repository.Search("Amsterdam", new DateTime(2017, 10, 13), new DateTime(2017, 10, 19), 4);
			Assert.Empty(searchResults);
		}

		[Fact]
		public void TestSearchDepartureAndArrivalReturnMulitpleExistingHoliday()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);

			context.Holidays.AddRange(new Holiday()
			{
				DepartureDate = new DateTime(2017, 10, 12),
				ReturnDate = new DateTime(2017, 10, 22),
				Accommodation = new Models.Accommodation()
				{
					Name = "Amsterdam",
					MaxPersons = 4
				}
			}, new Holiday()
			{
				DepartureDate = new DateTime(2017, 10, 19),
				ReturnDate = new DateTime(2017, 10, 22),
				Accommodation = new Models.Accommodation()
				{
					Name = "Amsterdam",
					MaxPersons = 5
				}
			});

			context.SaveChanges();

			IHolidayRepository repository = new EFHolidayRepository(context);

			IEnumerable<Holiday> searchResults = repository.Search("Amsterdam", new DateTime(2017, 10, 12), new DateTime(2017, 10, 22), 4);

			Assert.NotEmpty(searchResults);
			Assert.True(searchResults.Count() == 2);
		}

		[Fact]
		public void TestSearchNotExistingNrOfPersons()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);

			context.Holidays.Add(new Holiday()
			{
				DepartureDate = new DateTime(2017, 10, 12),
				ReturnDate = new DateTime(2017, 10, 22),
				Accommodation = new Models.Accommodation()
				{
					Name = "Amsterdam",
					MaxPersons = 4
				}
			});

			context.SaveChanges();

			IHolidayRepository repository = new EFHolidayRepository(context);

			IEnumerable<Holiday> searchResults = repository.Search("Amsterdam", new DateTime(2017, 10, 11), new DateTime(2017, 10, 23), 9);

			Assert.Empty(searchResults);
		}

		[Fact]
		public void TestSearchNotExistingLocationReturnNoHoliday()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);

			context.Holidays.Add(new Holiday()
			{
				DepartureDate = new DateTime(2017, 10, 12),
				ReturnDate = new DateTime(2017, 10, 22),
				Accommodation = new Models.Accommodation()
				{
					Name = "Amsterdam",
					MaxPersons = 4
				}
			});

			context.SaveChanges();

			IHolidayRepository repository = new EFHolidayRepository(context);

			IEnumerable<Holiday> searchResults = repository.Search("Rotterdam", new DateTime(2017, 10, 11), new DateTime(2017, 10, 23), 4);

			Assert.Empty(searchResults);
		}

		[Fact]
		public void TestSearchLocationSpacesReturnHoliday()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HolidayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HolidayDbContext context = new HolidayDbContext(optionsBuilder.Options);

			context.Holidays.Add(new Holiday()
			{
				DepartureDate = new DateTime(2017, 10, 12),
				ReturnDate = new DateTime(2017, 10, 22),
				Accommodation = new Models.Accommodation()
				{
					Name = "Amsterdam",
					MaxPersons = 4
				}
			});

			context.SaveChanges();

			IHolidayRepository repository = new EFHolidayRepository(context);

			IEnumerable<Holiday> searchResults = repository.Search("   Amsterdam   ", new DateTime(2017, 10, 11), new DateTime(2017, 10, 23), 4);

			Assert.NotEmpty(searchResults);
		}
	}
}
