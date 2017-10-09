using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeMyDay.Controllers.Cms;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeMyDay.Tests
{
	public class FaqCmsControllerTest
	{
		[Fact]
		public void TestEmptyFaqList()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IFaqRepository repository = new EFFaqRepository(context);

			var target = new FaqController(repository);
			var result = target.Index(1, 10).Result as ViewResult;
			var model = result.Model as IEnumerable<FaqCategory>;

			Assert.NotNull(model);
			Assert.True(!model.Any());
		}

		[Fact]
		public void TestFilledFaqList()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);

			context.FaqCategory.AddRange(
				new FaqCategory() { CategoryName = "TestA" },
				new FaqCategory() { CategoryName = "TestB" },
				new FaqCategory() { CategoryName = "TestC" }
			);
			context.SaveChanges();

			IFaqRepository repository = new EFFaqRepository(context);

			var target = new FaqController(repository);
			var result = target.Index(1, 10).Result as ViewResult;
			var model = result.Model as IEnumerable<FaqCategory>;

			Assert.NotNull(model);
			Assert.True(model.Count() == 3);
		}
	}
}
