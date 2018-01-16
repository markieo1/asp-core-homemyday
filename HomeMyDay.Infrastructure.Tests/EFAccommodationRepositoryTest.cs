using System;
using System.Collections.Generic;
using System.Linq;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Repository;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Web.Base.Managers.Implementation;
using HomeMyDay.Web.Site.Home.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace HomeMyDay.Infrastructure.Tests
{
	[TestClass]
	[Ignore]
	public class EFAccommodationRepositoryTest
	{
		private const string DEFAULT_ACCOMMODATION_DESCRIPTION = "Dit is een omschrijving";

		[TestMethod][Ignore]
		public void TestGetIdBelowZeroAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => repository.GetAccommodation("0"));
		}

		[TestMethod][Ignore]
		public void TestGetIdNotExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.Add(new Accommodation()
			{
				Location = "Amsterdam",
				MaxPersons = 4,
				Id = "1"
			});

			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Xunit.Assert.Throws<KeyNotFoundException>(() => repository.GetAccommodation("2"));
		}

		[TestMethod][Ignore]
		public void TestGetIdExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.Add(new Accommodation()
			{
				Location = "Amsterdam",
				MaxPersons = 4,
				Id = "1"
			});

			context.SaveChanges();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Accommodation accommodation = repository.GetAccommodation("1");

			Xunit.Assert.NotNull(accommodation);
			Xunit.Assert.Equal("Amsterdam", accommodation.Location);
		}

		[TestMethod][Ignore]
		public void TestAccommodationsEmptyRepository()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Xunit.Assert.Empty(repository.Accommodations);
		}

		[TestMethod][Ignore]
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

			Xunit.Assert.True(repository.Accommodations.Count() == 4);
		}

		[TestMethod][Ignore]
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
			IReviewRepository reviewRepository = new EFReviewRepository(context, repository);
			IAccommodationManager manager = new AccommodationManager(repository, reviewRepository);


			RecommendedAccommodationViewComponent component = new RecommendedAccommodationViewComponent(manager);

			IEnumerable<Accommodation> accommodations = ((IEnumerable<Accommodation>)(component.Invoke() as ViewViewComponentResult).ViewData.Model);

			Xunit.Assert.True(accommodations.Count() == 2);
		}

		[TestMethod][Ignore]
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
			IReviewRepository reviewRepository = new EFReviewRepository(context, repository);
			IAccommodationManager manager = new AccommodationManager(repository, reviewRepository);

			RecommendedAccommodationViewComponent component = new RecommendedAccommodationViewComponent(manager);

			IEnumerable<Accommodation> accommodations = ((IEnumerable<Accommodation>)(component.Invoke() as ViewViewComponentResult).ViewData.Model);

			Xunit.Assert.Empty(accommodations);
		}

		#region "Search"
		[TestMethod][Ignore]
		public void TestSearchEmptyLocation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Xunit.Assert.Throws<ArgumentNullException>(() => repository.Search("", DateTime.Now, DateTime.Now, 1));
		}

		[TestMethod][Ignore]
		public void TestSearchEmptyArrivalDate()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", new DateTime(), DateTime.Now, 1));
		}

		[TestMethod][Ignore]
		public void TestSearchEmptyLeaveDate()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", DateTime.Now, new DateTime(), 1));
		}

		[TestMethod][Ignore]
		public void TestSearchEmptyGuests()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Xunit.Assert.Throws<ArgumentNullException>(() => repository.Search("Amsterdam", DateTime.Now, DateTime.Now, 0));
		}

		[TestMethod][Ignore]
		public void TestSearchReturnAfterDeparture()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IAccommodationRepository repository = new EFAccommodationRepository(context);

			Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => repository.Search("Amsterdam", new DateTime(2017, 10, 12), new DateTime(2017, 10, 11), 4));
		}

		[TestMethod][Ignore]
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

			Xunit.Assert.NotEmpty(searchResults);

			Accommodation firstResult = searchResults.FirstOrDefault();
			Xunit.Assert.NotNull(firstResult);
			Xunit.Assert.True(firstResult.MaxPersons == 4);
			Xunit.Assert.True(firstResult.Location == "Amsterdam");
		}

		[TestMethod][Ignore]
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
			Xunit.Assert.Empty(searchResults);
		}

		[TestMethod][Ignore]
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

			Xunit.Assert.NotEmpty(searchResults);
			Xunit.Assert.True(searchResults.Count() == 2);
		}

		[TestMethod][Ignore]
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

			Xunit.Assert.Empty(searchResults);
		}

		[TestMethod][Ignore]
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

			Xunit.Assert.Empty(searchResults);
		}

		[TestMethod][Ignore]
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

			Xunit.Assert.NotEmpty(searchResults);
		}

		[TestMethod][Ignore]
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

			Xunit.Assert.NotEmpty(searchResults);
		}

		#endregion

		#region "List"

		[TestMethod][Ignore]
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

			Xunit.Assert.NotNull(paginatedAccommodations);
			Xunit.Assert.Equal(4, paginatedAccommodations.Count);
			Xunit.Assert.Equal(1, paginatedAccommodations.PageIndex);
			Xunit.Assert.Equal(10, paginatedAccommodations.PageSize);
			Xunit.Assert.Equal(1, paginatedAccommodations.TotalPages);
			Xunit.Assert.False(paginatedAccommodations.HasPreviousPage);
			Xunit.Assert.False(paginatedAccommodations.HasNextPage);
		}

		[TestMethod][Ignore]
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

			Xunit.Assert.NotNull(paginatedAccommodations);
			Xunit.Assert.Equal(1, paginatedAccommodations.Count);
			Xunit.Assert.Equal(1, paginatedAccommodations.PageIndex);
			Xunit.Assert.Equal(1, paginatedAccommodations.PageSize);
			Xunit.Assert.Equal(4, paginatedAccommodations.TotalPages);
			Xunit.Assert.False(paginatedAccommodations.HasPreviousPage);
			Xunit.Assert.True(paginatedAccommodations.HasNextPage);
		}

		[TestMethod][Ignore]
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

			Xunit.Assert.NotNull(paginatedAccommodations);
			Xunit.Assert.Equal(1, paginatedAccommodations.Count);
			Xunit.Assert.Equal(2, paginatedAccommodations.PageIndex);
			Xunit.Assert.Equal(1, paginatedAccommodations.PageSize);
			Xunit.Assert.Equal(4, paginatedAccommodations.TotalPages);
			Xunit.Assert.True(paginatedAccommodations.HasPreviousPage);
			Xunit.Assert.True(paginatedAccommodations.HasNextPage);
		}


		[TestMethod][Ignore]
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

			Xunit.Assert.NotNull(paginatedAccommodations);	   
			Xunit.Assert.Equal(1, paginatedAccommodations.Count);
			Xunit.Assert.Equal(1, paginatedAccommodations.PageIndex);
			Xunit.Assert.Equal(1, paginatedAccommodations.PageSize);
			Xunit.Assert.Equal(4, paginatedAccommodations.TotalPages);
			Xunit.Assert.False(paginatedAccommodations.HasPreviousPage);
			Xunit.Assert.True(paginatedAccommodations.HasNextPage);
		}

		[TestMethod][Ignore]
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

			Xunit.Assert.NotNull(paginatedAccommodations);
			Xunit.Assert.Equal(4, paginatedAccommodations.Count);
			Xunit.Assert.Equal(1, paginatedAccommodations.PageIndex);
			Xunit.Assert.Equal(10, paginatedAccommodations.PageSize);
			Xunit.Assert.Equal(1, paginatedAccommodations.TotalPages);
			Xunit.Assert.False(paginatedAccommodations.HasPreviousPage);
			Xunit.Assert.False(paginatedAccommodations.HasNextPage);
		}

		[TestMethod][Ignore]
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

			Xunit.Assert.NotNull(paginatedAccommodations);
			Xunit.Assert.Equal(4, paginatedAccommodations.Count);
			Xunit.Assert.Equal(1, paginatedAccommodations.PageIndex);
			Xunit.Assert.Equal(10, paginatedAccommodations.PageSize);
			Xunit.Assert.Equal(1, paginatedAccommodations.TotalPages);
			Xunit.Assert.False(paginatedAccommodations.HasPreviousPage);
			Xunit.Assert.False(paginatedAccommodations.HasNextPage);
		}

		[TestMethod][Ignore]
		public async void TestEmptyListPageOnePageSizeTen()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			PaginatedList<Accommodation> paginatedAccommodations = await repository.List(1, 10);

			Xunit.Assert.NotNull(paginatedAccommodations);
			Xunit.Assert.Empty(paginatedAccommodations);
			Xunit.Assert.Equal(1, paginatedAccommodations.PageIndex);
			Xunit.Assert.Equal(10, paginatedAccommodations.PageSize);
			Xunit.Assert.Equal(1, paginatedAccommodations.TotalPages);
			Xunit.Assert.False(paginatedAccommodations.HasPreviousPage);
			Xunit.Assert.False(paginatedAccommodations.HasNextPage);
		}

		[TestMethod][Ignore]
		public async void TestEmptyListPageBelowZeroPageSizeTen()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			PaginatedList<Accommodation> paginatedAccommodations = await repository.List(-5, 10);

			Xunit.Assert.NotNull(paginatedAccommodations);
			Xunit.Assert.Empty(paginatedAccommodations);
			Xunit.Assert.Equal(1, paginatedAccommodations.PageIndex);
			Xunit.Assert.Equal(10, paginatedAccommodations.PageSize);
			Xunit.Assert.Equal(1, paginatedAccommodations.TotalPages);
			Xunit.Assert.False(paginatedAccommodations.HasPreviousPage);
			Xunit.Assert.False(paginatedAccommodations.HasNextPage);
		}

		[TestMethod][Ignore]
		public async void TestEmptyListPageBelowZeroPageSizeBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			PaginatedList<Accommodation> paginatedAccommodations = await repository.List(-5, -10);

			Xunit.Assert.NotNull(paginatedAccommodations);
			Xunit.Assert.Empty(paginatedAccommodations);
			Xunit.Assert.Equal(1, paginatedAccommodations.PageIndex);
			Xunit.Assert.Equal(10, paginatedAccommodations.PageSize);
			Xunit.Assert.Equal(1, paginatedAccommodations.TotalPages);
			Xunit.Assert.False(paginatedAccommodations.HasPreviousPage);
			Xunit.Assert.False(paginatedAccommodations.HasNextPage);
		}
		#endregion

		#region "Save"

		[TestMethod][Ignore]
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

			await Xunit.Assert.ThrowsAsync<ArgumentNullException>(() => repository.Save(null));
		}

		[TestMethod][Ignore]
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

			Xunit.Assert.NotNull(foundAccommodation);
			Xunit.Assert.Equal("Example Name", foundAccommodation.Name);
			Xunit.Assert.NotEqual("0", foundAccommodation.Id);
			Xunit.Assert.Equal("Unknown", foundAccommodation.Country);
			Xunit.Assert.Equal(DEFAULT_ACCOMMODATION_DESCRIPTION, foundAccommodation.Description);
		}

		[TestMethod][Ignore]
		public async void TestSaveUpdatedAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			Accommodation accommodationToUpdate = new Accommodation()
			{
				Id = "1",
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

			Xunit.Assert.NotNull(updatedAccommodation);
			Xunit.Assert.Equal("Updated name", updatedAccommodation.Name);
			Xunit.Assert.Equal("The Netherlands", updatedAccommodation.Country);
			Xunit.Assert.Equal("Updated description", updatedAccommodation.Description);
			Xunit.Assert.Equal(2, updatedAccommodation.MaxPersons);
		}

		[TestMethod][Ignore]
		public async void TestSaveNotExistingAccommodationWithNotExistingId()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			Accommodation existingAccommodation = new Accommodation()
			{
				Id = "2",
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
				Id = "3",
				Country = "The Netherlands",
				Name = "Updated name",
				Description = "Updated description",
				MaxPersons = 2
			};

			await Xunit.Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => repository.Save(accommodationToUpdate));
		}

		#endregion

		#region "Delete"

		[TestMethod][Ignore]
		public async void TestDeleteExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Id = "1", Description = DEFAULT_ACCOMMODATION_DESCRIPTION }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			await repository.Delete("1");

			Accommodation deletedAccommodation = await context.Accommodations.FindAsync((long)1);
			Xunit.Assert.Null(deletedAccommodation);
		}

		[TestMethod][Ignore]
		public async void TestDeleteNotExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Id = "1", Description = DEFAULT_ACCOMMODATION_DESCRIPTION }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			await Xunit.Assert.ThrowsAsync<ArgumentNullException>(() => repository.Delete("2"));
		}

		[TestMethod][Ignore]
		public async void TestDeleteIdBelowZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Id = "1", Description = DEFAULT_ACCOMMODATION_DESCRIPTION }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			await Xunit.Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => repository.Delete("-10"));
		}

		[TestMethod][Ignore]
		public async void TestDeleteIdEqualsZero()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.Accommodations.AddRange(
				new Accommodation() { Id = "1", Description = DEFAULT_ACCOMMODATION_DESCRIPTION }
			);

			await context.SaveChangesAsync();

			IAccommodationRepository repository = new EFAccommodationRepository(context);

			await Xunit.Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => repository.Delete("0"));
		}

		#endregion
	}
}
