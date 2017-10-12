using System;
using System.Collections.Generic;
using System.Linq;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Repository;
using HomeMyDay.Web.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace HomeMyDay.Infrastructure.Tests
{
	public class EFAccommodationRepositoryTest
	{
		private const string DEFAULT_ACCOMMODATION_DESCRIPTION = "Dit is een omschrijving";

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
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = true },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = true }
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
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = true },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = true }
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
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false }
			);
			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			RecommendedAccommodationViewComponent component = new RecommendedAccommodationViewComponent(repository);

			IEnumerable<Accommodation> accommodations = ((IEnumerable<Accommodation>)(component.Invoke() as ViewViewComponentResult).ViewData.Model);

			Assert.Empty(accommodations);
		}

		#region "Search"
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

		[Fact]
		public void TestSearchAcccommodationWithoutNotAvailableDates()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.Add(new Accommodation()
			{
				Location = "Amsterdam",
				MaxPersons = 4
			});

			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			IEnumerable<Accommodation> searchResults = repository.Search("Amsterdam", new DateTime(2017, 10, 11), new DateTime(2017, 10, 23), 4);

			Assert.NotEmpty(searchResults);
		}

		#endregion

		#region "List"

		[Fact]
		public async void TestListWithItemsOnePageSizeTen()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			PaginatedList<Accommodation> paginatedAccommodations = await repository.List(1, 10);

			Assert.NotNull(paginatedAccommodations);
			Assert.Equal(4, paginatedAccommodations.Count);
			Assert.Equal(1, paginatedAccommodations.PageIndex);
			Assert.Equal(10, paginatedAccommodations.PageSize);
			Assert.Equal(1, paginatedAccommodations.TotalPages);
			Assert.False(paginatedAccommodations.HasPreviousPage);
			Assert.False(paginatedAccommodations.HasNextPage);
		}

		[Fact]
		public async void TestListWithItemsMultiplePagesSizeOne()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			PaginatedList<Accommodation> paginatedAccommodations = await repository.List(1, 1);

			Assert.NotNull(paginatedAccommodations);
			Assert.Equal(1, paginatedAccommodations.Count);
			Assert.Equal(1, paginatedAccommodations.PageIndex);
			Assert.Equal(1, paginatedAccommodations.PageSize);
			Assert.Equal(4, paginatedAccommodations.TotalPages);
			Assert.False(paginatedAccommodations.HasPreviousPage);
			Assert.True(paginatedAccommodations.HasNextPage);
		}

		[Fact]
		public async void TestListWithItemsMultiplePagesSizeOneWithPreviousPage()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			PaginatedList<Accommodation> paginatedAccommodations = await repository.List(2, 1);

			Assert.NotNull(paginatedAccommodations);
			Assert.Equal(1, paginatedAccommodations.Count);
			Assert.Equal(2, paginatedAccommodations.PageIndex);
			Assert.Equal(1, paginatedAccommodations.PageSize);
			Assert.Equal(4, paginatedAccommodations.TotalPages);
			Assert.True(paginatedAccommodations.HasPreviousPage);
			Assert.True(paginatedAccommodations.HasNextPage);
		}


		[Fact]
		public async void TestListWithItemsPageBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			PaginatedList<Accommodation> paginatedAccommodations = await repository.List(-5, 1);

			Assert.NotNull(paginatedAccommodations);
			Assert.Equal(1, paginatedAccommodations.Count);
			Assert.Equal(1, paginatedAccommodations.PageIndex);
			Assert.Equal(1, paginatedAccommodations.PageSize);
			Assert.Equal(4, paginatedAccommodations.TotalPages);
			Assert.False(paginatedAccommodations.HasPreviousPage);
			Assert.True(paginatedAccommodations.HasNextPage);
		}

		[Fact]
		public async void TestListWithItemsPageSizeBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			PaginatedList<Accommodation> paginatedAccommodations = await repository.List(1, -10);

			Assert.NotNull(paginatedAccommodations);
			Assert.Equal(4, paginatedAccommodations.Count);
			Assert.Equal(1, paginatedAccommodations.PageIndex);
			Assert.Equal(10, paginatedAccommodations.PageSize);
			Assert.Equal(1, paginatedAccommodations.TotalPages);
			Assert.False(paginatedAccommodations.HasPreviousPage);
			Assert.False(paginatedAccommodations.HasNextPage);
		}

		[Fact]
		public async void TestListWithItemsPageBelowZeroPageSizeBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false },
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION, Recommended = false }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			PaginatedList<Accommodation> paginatedAccommodations = await repository.List(-8, -10);

			Assert.NotNull(paginatedAccommodations);
			Assert.Equal(4, paginatedAccommodations.Count);
			Assert.Equal(1, paginatedAccommodations.PageIndex);
			Assert.Equal(10, paginatedAccommodations.PageSize);
			Assert.Equal(1, paginatedAccommodations.TotalPages);
			Assert.False(paginatedAccommodations.HasPreviousPage);
			Assert.False(paginatedAccommodations.HasNextPage);
		}

		[Fact]
		public async void TestEmptyListPageOnePageSizeTen()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			PaginatedList<Accommodation> paginatedAccommodations = await repository.List(1, 10);

			Assert.NotNull(paginatedAccommodations);
			Assert.Equal(0, paginatedAccommodations.Count);
			Assert.Equal(1, paginatedAccommodations.PageIndex);
			Assert.Equal(10, paginatedAccommodations.PageSize);
			Assert.Equal(1, paginatedAccommodations.TotalPages);
			Assert.False(paginatedAccommodations.HasPreviousPage);
			Assert.False(paginatedAccommodations.HasNextPage);
		}

		[Fact]
		public async void TestEmptyListPageBelowZeroPageSizeTen()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			PaginatedList<Accommodation> paginatedAccommodations = await repository.List(-5, 10);

			Assert.NotNull(paginatedAccommodations);
			Assert.Equal(0, paginatedAccommodations.Count);
			Assert.Equal(1, paginatedAccommodations.PageIndex);
			Assert.Equal(10, paginatedAccommodations.PageSize);
			Assert.Equal(1, paginatedAccommodations.TotalPages);
			Assert.False(paginatedAccommodations.HasPreviousPage);
			Assert.False(paginatedAccommodations.HasNextPage);
		}

		[Fact]
		public async void TestEmptyListPageBelowZeroPageSizeBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			PaginatedList<Accommodation> paginatedAccommodations = await repository.List(-5, -10);

			Assert.NotNull(paginatedAccommodations);
			Assert.Equal(0, paginatedAccommodations.Count);
			Assert.Equal(1, paginatedAccommodations.PageIndex);
			Assert.Equal(10, paginatedAccommodations.PageSize);
			Assert.Equal(1, paginatedAccommodations.TotalPages);
			Assert.False(paginatedAccommodations.HasPreviousPage);
			Assert.False(paginatedAccommodations.HasNextPage);
		}
		#endregion

		#region "Save"

		[Fact]
		public async void TestSaveNullAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			await Assert.ThrowsAsync<ArgumentNullException>(() => repository.Save(null));
		}

		[Fact]
		public async void TestSaveNewAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Description = DEFAULT_ACCOMMODATION_DESCRIPTION }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Accommodation accommodationToCreate = new Accommodation()
			{
				Description = DEFAULT_ACCOMMODATION_DESCRIPTION,
				Name = "Example Name",
				Country = "Unknown"
			};

			await repository.Save(accommodationToCreate);

			// Check if the item was created
			Accommodation foundAccommodation = await context.Accommodations.FirstOrDefaultAsync(x => x.Name == "Example Name");

			Assert.NotNull(foundAccommodation);
			Assert.Equal("Example Name", foundAccommodation.Name);
			Assert.NotEqual(0, foundAccommodation.Id);
			Assert.Equal("Unknown", foundAccommodation.Country);
			Assert.Equal(DEFAULT_ACCOMMODATION_DESCRIPTION, foundAccommodation.Description);
		}

		[Fact]
		public async void TestSaveUpdatedAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			Accommodation accommodationToUpdate = new Accommodation()
			{
				Id = 1,
				Description = DEFAULT_ACCOMMODATION_DESCRIPTION,
				Name = "Example Name",
				Country = "Unknown",
				MaxPersons = 2
			};

			await context.Accommodations.AddAsync(accommodationToUpdate);
			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			// Change some values
			accommodationToUpdate.Country = "The Netherlands";
			accommodationToUpdate.Name = "Updated name";
			accommodationToUpdate.Description = "Updated description";

			await repository.Save(accommodationToUpdate);

			// Check if the item was updated
			Accommodation updatedAccommodation = await context.Accommodations.FindAsync((long)1);

			Assert.NotNull(updatedAccommodation);
			Assert.Equal("Updated name", updatedAccommodation.Name);
			Assert.Equal("The Netherlands", updatedAccommodation.Country);
			Assert.Equal("Updated description", updatedAccommodation.Description);
			Assert.Equal(2, updatedAccommodation.MaxPersons);
		}

		[Fact]
		public async void TestSaveNotExistingAccommodationWithNotExistingId()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			Accommodation existingAccommodation = new Accommodation()
			{
				Id = 2,
				Description = DEFAULT_ACCOMMODATION_DESCRIPTION,
				Name = "Example Name",
				Country = "Unknown",
				MaxPersons = 2
			};

			await context.Accommodations.AddAsync(existingAccommodation);
			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			// Change some values
			Accommodation accommodationToUpdate = new Accommodation()
			{
				Id = 3,
				Country = "The Netherlands",
				Name = "Updated name",
				Description = "Updated description",
				MaxPersons = 2
			};

			await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => repository.Save(accommodationToUpdate));
		}

		#endregion

		#region "Delete"

		[Fact]
		public async void TestDeleteExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Id = 1, Description = DEFAULT_ACCOMMODATION_DESCRIPTION }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			await repository.Delete(1);

			Accommodation deletedAccommodation = await context.Accommodations.FindAsync((long)1);
			Assert.Null(deletedAccommodation);
		}

		[Fact]
		public async void TestDeleteNotExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Id = 1, Description = DEFAULT_ACCOMMODATION_DESCRIPTION }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			await Assert.ThrowsAsync<ArgumentNullException>(() => repository.Delete(2));
		}

		[Fact]
		public async void TestDeleteIdBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Id = 1, Description = DEFAULT_ACCOMMODATION_DESCRIPTION }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => repository.Delete(-10));
		}

		[Fact]
		public async void TestDeleteIdEqualsZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Id = 1, Description = DEFAULT_ACCOMMODATION_DESCRIPTION }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => repository.Delete(0));
		}

		#endregion
	}
}
