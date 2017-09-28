using HomeMyDay.Components;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HomeMyDay.Tests
{
	public class EFAccommodationRepositoryTest
	{
		[Fact]
		public void TestGetIdBelowZeroAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.GetAccommodation(0));
		}

		[Fact]
		public void TestGetIdNotExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

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
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

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

		[Fact]
		public void TestAccommodationsEmptyRepository()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Assert.Empty(repository.Accommodations);
		}

		[Fact]
		public void TestAccommodationsFilledRepository()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Description = "Dit is een omschrijving", Recommended = false },
				new Accommodation() { Description = "Dit is een omschrijving", Recommended = true },
				new Accommodation() { Description = "Dit is een omschrijving", Recommended = false },
				new Accommodation() { Description = "Dit is een omschrijving", Recommended = true }
			);
			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Assert.True(repository.Accommodations.Count() == 4);
		}

		[Fact]
		public void TestAccommodationsTrueRecommended()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Description = "Dit is een omschrijving", Recommended = false },
				new Accommodation() { Description = "Dit is een omschrijving", Recommended = true },
				new Accommodation() { Description = "Dit is een omschrijving", Recommended = false },
				new Accommodation() { Description = "Dit is een omschrijving", Recommended = true }
			);
			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			RecommendedAccommodationViewComponent component = new RecommendedAccommodationViewComponent(repository);

			IEnumerable<Accommodation> accommodations = ((IEnumerable<Accommodation>)(component.Invoke() as ViewViewComponentResult).ViewData.Model);

			Assert.True(accommodations.Count() == 2);
		}

		[Fact]
		public void TestAccommodationsFalseRecommended()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Description = "Dit is een omschrijving", Recommended = false },
				new Accommodation() { Description = "Dit is een omschrijving", Recommended = false },
				new Accommodation() { Description = "Dit is een omschrijving", Recommended = false },
				new Accommodation() { Description = "Dit is een omschrijving", Recommended = false }
			);
			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			RecommendedAccommodationViewComponent component = new RecommendedAccommodationViewComponent(repository);

			IEnumerable<Accommodation> accommodations = ((IEnumerable<Accommodation>)(component.Invoke() as ViewViewComponentResult).ViewData.Model);

			Assert.Empty(accommodations);
		}

		[Fact]
		public void TestSearchEmptyLocation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Assert.Throws<ArgumentNullException>(() => repository.Search("", DateTime.Now, DateTime.Now, 1));
		}

		[Fact]
		public void TestSearchEmptyArrivalDate()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", new DateTime(), DateTime.Now, 1));
		}

		[Fact]
		public void TestSearchEmptyLeaveDate()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", DateTime.Now, new DateTime(), 1));
		}

		[Fact]
		public void TestSearchEmptyGuests()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Assert.Throws<ArgumentNullException>(() => repository.Search("Amsterdam", DateTime.Now, DateTime.Now, 0));
		}

		[Fact]
		public void TestSearchReturnAfterDeparture()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", new DateTime(2017, 10, 12), new DateTime(2017, 10, 11), 4));
		}

		[Fact]
		public void TestSearchDepartureAndArrivalReturnExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.Add(new Accommodation()
			{
				NotAvailableDates = new List<DateEntity>()
				{
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 11)
					},
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 23)
					},
				},
				Location = "Amsterdam",
				MaxPersons = 4
			});

			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			IEnumerable<Accommodation> searchResults = repository.Search("Amsterdam", new DateTime(2017, 10, 11), new DateTime(2017, 10, 23), 4);

			Assert.NotEmpty(searchResults);

			Accommodation firstResult = searchResults.FirstOrDefault();
			Assert.NotNull(firstResult);
			Assert.True(firstResult.MaxPersons == 4);
			Assert.True(firstResult.Location == "Amsterdam");
		}

		[Fact]
		public void TestSearchDepartureAndArrivalReturnNoAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.Add(new Accommodation()
			{
				NotAvailableDates = new List<DateEntity>()
				{
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 12)
					},new DateEntity()
					{
						Date= new DateTime(2017, 10, 13)
					},new DateEntity()
					{
						Date= new DateTime(2017, 10, 14)
					},new DateEntity()
					{
						Date= new DateTime(2017, 10, 15)
					},new DateEntity()
					{
						Date= new DateTime(2017, 10, 16)
					},new DateEntity()
					{
						Date= new DateTime(2017, 10, 17)
					},
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 18)
					},new DateEntity()
					{
						Date= new DateTime(2017, 10, 19)
					},new DateEntity()
					{
						Date= new DateTime(2017, 10, 20)
					},new DateEntity()
					{
						Date= new DateTime(2017, 10, 21)
					},
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 22)
					},
				},
				Name = "Amsterdam",
				MaxPersons = 4
			});

			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			IEnumerable<Accommodation> searchResults = repository.Search("Amsterdam", new DateTime(2017, 10, 13), new DateTime(2017, 10, 19), 4);
			Assert.Empty(searchResults);
		}

		[Fact]
		public void TestSearchDepartureAndArrivalReturnMulitpleExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(new Accommodation()
			{
				NotAvailableDates = new List<DateEntity>()
				{
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 11)
					},
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 23)
					},
				},
				Location = "Amsterdam",
				MaxPersons = 4
			},
			new Accommodation()
			{
				NotAvailableDates = new List<DateEntity>()
				{
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 12)
					},new DateEntity()
					{
						Date= new DateTime(2017, 10, 13)
					},new DateEntity()
					{
						Date= new DateTime(2017, 10, 14)
					},new DateEntity()
					{
						Date= new DateTime(2017, 10, 15)
					},new DateEntity()
					{
						Date= new DateTime(2017, 10, 16)
					},new DateEntity()
					{
						Date= new DateTime(2017, 10, 17)
					},
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 18)
					}
				},
				Location = "Amsterdam",
				MaxPersons = 5

			});

			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			IEnumerable<Accommodation> searchResults = repository.Search("Amsterdam", new DateTime(2017, 10, 12), new DateTime(2017, 10, 22), 4);

			Assert.NotEmpty(searchResults);
			Assert.True(searchResults.Count() == 2);
		}

		[Fact]
		public void TestSearchNotExistingNrOfPersons()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.Add(new Accommodation()
			{
				NotAvailableDates = new List<DateEntity>()
				{
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 11)
					},
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 23)
					},
				},
				Name = "Amsterdam",
				MaxPersons = 4
			});

			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			IEnumerable<Accommodation> searchResults = repository.Search("Amsterdam", new DateTime(2017, 10, 11), new DateTime(2017, 10, 23), 9);

			Assert.Empty(searchResults);
		}

		[Fact]
		public void TestSearchNotExistingLocationReturnNoAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.Add(new Accommodation()
			{
				NotAvailableDates = new List<DateEntity>()
				{
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 11)
					},
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 23)
					},
				},
				Name = "Amsterdam",
				MaxPersons = 4
			});

			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			IEnumerable<Accommodation> searchResults = repository.Search("Rotterdam", new DateTime(2017, 10, 11), new DateTime(2017, 10, 23), 4);

			Assert.Empty(searchResults);
		}

		[Fact]
		public void TestSearchLocationSpacesReturnAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.Add(new Accommodation()
			{
				NotAvailableDates = new List<DateEntity>()
				{
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 11)
					},
					new DateEntity()
					{
						Date= new DateTime(2017, 10, 23)
					},
				},
				Location = "Amsterdam",
				MaxPersons = 4
			});

			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			IEnumerable<Accommodation> searchResults = repository.Search("   Amsterdam   ", new DateTime(2017, 10, 11), new DateTime(2017, 10, 23), 4);

			Assert.NotEmpty(searchResults);
		}
	}
}
