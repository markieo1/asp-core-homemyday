using System;
using System.Collections.Generic;
using System.Linq;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HomeMyDay.Tests
{
	public class EFReviewRepositoryTest
	{
		[Fact]
		public void TestGetIdBelowZeroReview()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			var accommodationRepository = new Mock<IAccommodationRepository>();
			accommodationRepository.SetupGet(x => x.Accommodations).Returns(new List<Accommodation>()
			{
				new Accommodation()
				{
					Id = 1,
					Name = "Test",
					Beds = 2,
					Country = "Breda",
					MaxPersons = 6
				},
				new Accommodation()
				{
					Id = 2,
					Name = "Test2",
					Beds = 4,
					Country = "Tilburg",
					MaxPersons = 4
				}
			});

			IReviewRepository repository = new EFReviewRepository(context, accommodationRepository.Object);
			Assert.Throws<ArgumentOutOfRangeException>(() => repository.GetAccomodationReviews(-2));
		}

		[Fact]
		public void TestGetIdIsZeroReview()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			var accommodationRepository = new Mock<IAccommodationRepository>();
			accommodationRepository.SetupGet(x => x.Accommodations).Returns(new List<Accommodation>()
			{
				new Accommodation()
				{
					Id = 1,
					Name = "Test",
					Beds = 2,
					Country = "Breda",
					MaxPersons = 6
				},
				new Accommodation()
				{
					Id = 2,
					Name = "Test2",
					Beds = 4,
					Country = "Tilburg",
					MaxPersons = 4
				}
			});

			IReviewRepository repository = new EFReviewRepository(context, accommodationRepository.Object);
			Assert.Throws<ArgumentOutOfRangeException>(() => repository.GetAccomodationReviews(0));

		}

		[Fact]
		public void TestGetExistingId()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			context.Reviews.Add(
				new Review()
				{
					Id = 1,
					Accommodation = new Accommodation()
					{ Id = 1, Name = "Test" },
					Name = "TestReview",
					Approved = true
				});
			context.SaveChanges();

			IReviewRepository repository = new EFReviewRepository(context, null);
			var reviews = repository.GetAccomodationReviews(1);
			Assert.NotNull(reviews.FirstOrDefault());
			Assert.Equal("Test", reviews.FirstOrDefault().Accommodation.Name);
			Assert.Equal("TestReview", reviews.FirstOrDefault().Name);
			Assert.True(reviews.Count() == 1);
		}

		[Fact]
		public void TestGetNotExistingId()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			context.Reviews.Add(new Review()
			{
				Id = 1,
				Accommodation = new Accommodation()
				{ Id = 1, Name = "Test" },
				Name = "TestReview",
				Approved = true
			});
			context.SaveChanges();
			IReviewRepository repository = new EFReviewRepository(context, null);
			var reviews = repository.GetAccomodationReviews(2);
			Assert.Empty(reviews);
		}

		[Fact]
		public void TestGetIdNotExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			context.Reviews.Add(new Review()
			{
				Id = 1,
				Accommodation = new Accommodation()
				{ Id = 1, Name = "Test" },
				Name = "TestReview",
				Approved = true
			});
			context.SaveChanges();
			IReviewRepository repository = new EFReviewRepository(context, null);
			var reviews = repository.GetAccomodationReviews(2);
			Assert.Empty(reviews);
		}

		[Fact]
		public void TestGetIdExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			var accommodation = new Accommodation()
			{
				Id = 1,
				Name = "TestAcco",
				Location = "Breda",
				Beds = 6
			};

			context.Reviews.Add(new Review()
			{
				Id = 1,
				Accommodation = accommodation,
				Name = "TestReview",
				Approved = true
			});
			context.Reviews.Add(new Review()
			{
				Id = 2,
				Name = "TestReview2",
				Accommodation = accommodation,
				Approved = false
			});
			context.SaveChanges();

			IReviewRepository repository = new EFReviewRepository(context, null);
			var reviews = repository.GetAccomodationReviews(1);
			Assert.NotEmpty(reviews);
			Assert.True(reviews.Count() == 2);
			Assert.True(reviews.All(x => x.Accommodation.Id == 1));
		}

		[Fact]
		public void TestGetArgumentExceptionReview()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			var accommodation = new Accommodation()
			{
				Id = 1,
				Name = "TestAcco",
				Location = "Breda",
				Beds = 6
			};

			context.Reviews.Add(new Review()
			{
				Id = 1,
				Accommodation = accommodation,
				Name = "TestReview",
				Approved = true
			});
			context.Reviews.Add(new Review()
			{
				Id = 2,
				Name = "TestReview2",
				Accommodation = accommodation,
				Approved = false
			});
			context.SaveChanges();

			IReviewRepository repository = new EFReviewRepository(context, null);
			Assert.Throws<ArgumentOutOfRangeException>(() => repository.GetAccomodationReviews(0));
		}

		[Fact]
		public void TestGetEmptyReviewRepository()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IReviewRepository repository = new EFReviewRepository(context, null);
			Assert.Empty(repository.GetAccomodationReviews(1));
			Assert.True(!repository.GetAccomodationReviews(1).Any());
		}

		[Fact]
		public void TestGetFilledReviewRepository()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			var accommodation = new Accommodation()
			{
				Id = 1,
				Name = "TestAcco",
				Location = "Breda",
				Beds = 6
			};

			context.Reviews.Add(new Review()
			{
				Id = 1,
				Accommodation = accommodation,
				Name = "TestReview",
				Approved = true
			});
			context.Reviews.Add(new Review()
			{
				Id = 2,
				Name = "TestReview2",
				Accommodation = accommodation,
				Approved = false
			});
			context.SaveChanges();

			IReviewRepository repository = new EFReviewRepository(context, null);
			Assert.NotEmpty(repository.GetAccomodationReviews(1));
			Assert.True(repository.GetAccomodationReviews(1).Count() == 2);
		}

		[Fact]
		public void TestAddNotExistingReview()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IAccommodationRepository accommodationRepository = new EFAccommodationRepository(context);

			context.Accommodations.Add(new Accommodation()
			{
				Id = 1,
				Name = "TestAccommodation",
				Beds = 6,
				Country = "Amsterdam",
				Rooms = 3
			});

			context.SaveChanges();

			IReviewRepository repository = new EFReviewRepository(context, accommodationRepository);
			Assert.True(repository.AddReview(accommodationRepository.GetAccommodation(1), "Review Holiday 001", "TestReview", "De vakantie was goed!"));
			Assert.True(repository.GetAccomodationReviews(1).Count() == 1);
		}

		[Fact]
		public void TestAddKeyNotFoundExceptionReview()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IAccommodationRepository accommodationRepository = new EFAccommodationRepository(context);

			context.Accommodations.Add(new Accommodation()
			{
				Id = 1,
				Name = "TestAccommodation",
				Beds = 6,
				Country = "Amsterdam",
				Rooms = 3
			});

			context.SaveChanges();

			IReviewRepository repository = new EFReviewRepository(context, accommodationRepository);
			Assert.Throws<KeyNotFoundException>(() => repository.AddReview(new Accommodation() { Id = 2 }, null, null, null));
		}

		[Fact]
		public void TestAddArgumentNullExceptionReview()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IAccommodationRepository accommodationRepository = new EFAccommodationRepository(context);

			context.Accommodations.Add(new Accommodation()
			{
				Id = 1,
				Name = "TestAccommodation",
				Beds = 6,
				Country = "Amsterdam",
				Rooms = 3
			});

			context.SaveChanges();

			IReviewRepository repository = new EFReviewRepository(context, accommodationRepository);
			Assert.Throws<ArgumentNullException>(() => repository.AddReview(null, null, null, null));
		}

		[Fact]
		public void TestAddArgumentOutOfRangeExceptionReview()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IAccommodationRepository accommodationRepository = new EFAccommodationRepository(context);

			context.Accommodations.Add(new Accommodation()
			{
				Id = 1,
				Name = "TestAccommodation",
				Beds = 6,
				Country = "Amsterdam",
				Rooms = 3
			});

			context.SaveChanges();

			IReviewRepository repository = new EFReviewRepository(context, accommodationRepository);
			Assert.Throws<ArgumentOutOfRangeException>(() => repository.AddReview(new Accommodation() { Id = 0 }, null, null, null));
		}
	}
}
