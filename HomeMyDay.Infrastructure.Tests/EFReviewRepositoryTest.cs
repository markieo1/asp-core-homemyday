using System;
using System.Collections.Generic;
using System.Linq;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;

namespace HomeMyDay.Infrastructure.Tests
{
	[TestClass]
	[Ignore]
	public class EFReviewRepositoryTest
	{
		[TestMethod][Ignore]
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
					Id = "1",
					Name = "Test",
					Beds = 2,
					Country = "Breda",
					MaxPersons = 6
				},
				new Accommodation()
				{
					Id = "2",
					Name = "Test2",
					Beds = 4,
					Country = "Tilburg",
					MaxPersons = 4
				}
			});

			IReviewRepository repository = new EFReviewRepository(context, accommodationRepository.Object);
			Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => repository.GetAccomodationReviews("-2"));
		}

		[TestMethod][Ignore]
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
					Id = "1",
					Name = "Test",
					Beds = 2,
					Country = "Breda",
					MaxPersons = 6
				},
				new Accommodation()
				{
					Id = "2",
					Name = "Test2",
					Beds = 4,
					Country = "Tilburg",
					MaxPersons = 4
				}
			});

			IReviewRepository repository = new EFReviewRepository(context, accommodationRepository.Object);
			Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => repository.GetAccomodationReviews("0"));

		}

		[TestMethod][Ignore]
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
					{ Id = "1", Name = "Test" },
					Name = "TestReview",
					Approved = true
				});
			context.SaveChanges();

			IReviewRepository repository = new EFReviewRepository(context, null);
			var reviews = repository.GetAccomodationReviews("1");
			Xunit.Assert.NotNull(reviews.FirstOrDefault());
			Xunit.Assert.Equal("Test", reviews.FirstOrDefault().Accommodation.Name);
			Xunit.Assert.Equal("TestReview", reviews.FirstOrDefault().Name);
			Xunit.Assert.True(reviews.Count() == 1);
		}

		[TestMethod][Ignore]
		public void TestGetNotExistingId()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			context.Reviews.Add(new Review()
			{
				Id = 1,
				Accommodation = new Accommodation()
				{ Id = "1", Name = "Test" },
				Name = "TestReview",
				Approved = true
			});
			context.SaveChanges();
			IReviewRepository repository = new EFReviewRepository(context, null);
			var reviews = repository.GetAccomodationReviews("2");
			Xunit.Assert.Empty(reviews);
		}

		[TestMethod][Ignore]
		public void TestGetIdNotExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			context.Reviews.Add(new Review()
			{
				Id = 1,
				Accommodation = new Accommodation()
				{ Id = "1", Name = "Test" },
				Name = "TestReview",
				Approved = true
			});
			context.SaveChanges();
			IReviewRepository repository = new EFReviewRepository(context, null);
			var reviews = repository.GetAccomodationReviews("2");
			Xunit.Assert.Empty(reviews);
		}

		[TestMethod][Ignore]
		public void TestGetIdExistingAccommodation()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			var accommodation = new Accommodation()
			{
				Id = "1",
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
			var reviews = repository.GetAccomodationReviews("1");
			Xunit.Assert.NotEmpty(reviews);
			Xunit.Assert.True(reviews.Count() == 2);
			Xunit.Assert.True(reviews.All(x => x.Accommodation.Id == "1"));
		}

		[TestMethod][Ignore]
		public void TestGetArgumentExceptionReview()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			var accommodation = new Accommodation()
			{
				Id = "1",
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
			Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => repository.GetAccomodationReviews("0"));
		}

		[TestMethod][Ignore]
		public void TestGetEmptyReviewRepository()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IReviewRepository repository = new EFReviewRepository(context, null);
			Xunit.Assert.Empty(repository.GetAccomodationReviews("1"));
			Xunit.Assert.True(!repository.GetAccomodationReviews("1").Any());
		}

		[TestMethod][Ignore]
		public void TestGetFilledReviewRepository()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			var accommodation = new Accommodation()
			{
				Id = "1",
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
			Xunit.Assert.NotEmpty(repository.GetAccomodationReviews("1"));
			Xunit.Assert.True(repository.GetAccomodationReviews("1").Count() == 2);
		}

		[TestMethod][Ignore]
		public void TestAddNotExistingReview()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IAccommodationRepository accommodationRepository = new EFAccommodationRepository(context);

			context.Accommodations.Add(new Accommodation()
			{
				Id = "1",
				Name = "TestAccommodation",
				Beds = 6,
				Country = "Amsterdam",
				Rooms = 3
			});

			context.SaveChanges();

			IReviewRepository repository = new EFReviewRepository(context, accommodationRepository);
			Xunit.Assert.True(repository.AddReview("1", "Review Holiday 001", "TestReview", "De vakantie was goed!"));
			Xunit.Assert.True(repository.GetAccomodationReviews("1").Count() == 1);
		}

		[TestMethod][Ignore]
		public void TestAddKeyNotFoundExceptionReview()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IAccommodationRepository accommodationRepository = new EFAccommodationRepository(context);

			context.Accommodations.Add(new Accommodation()
			{
				Id = "1",
				Name = "TestAccommodation",
				Beds = 6,
				Country = "Amsterdam",
				Rooms = 3
			});

			context.SaveChanges();

			IReviewRepository repository = new EFReviewRepository(context, accommodationRepository);
			Xunit.Assert.Throws<KeyNotFoundException>(() => repository.AddReview("2", null, null, null));
		}
		  
		[TestMethod][Ignore]
		public void TestAddArgumentOutOfRangeExceptionReview()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			IAccommodationRepository accommodationRepository = new EFAccommodationRepository(context);

			context.Accommodations.Add(new Accommodation()
			{
				Id = "1",
				Name = "TestAccommodation",
				Beds = 6,
				Country = "Amsterdam",
				Rooms = 3
			});

			context.SaveChanges();

			IReviewRepository repository = new EFReviewRepository(context, accommodationRepository);
			Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => repository.AddReview("0", null, null, null));
		}
	}
}
