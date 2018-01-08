using System;
using System.Collections.Generic;
using System.Linq;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Repository;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Web.Base.Managers.Implementation;
using HomeMyDay.Web.Site.Home.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeMyDay.Web.Site.Home.Tests
{
	public class FaqControllerTest
	{
		[Fact]
		public void TestEmptyFaqRepository()
		{
			var optionsBuilder = new DbContextOptionsBuilder<HomeMyDayDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			HomeMyDayDbContext context = new HomeMyDayDbContext(optionsBuilder.Options);
			IFaqRepository repo = new EFFaqRepository(context);
			IFaqManager manager = new FaqManager(repo);
			var target = new FaqController(manager);

			var result = target.Index() as ViewResult;
			var model = result.Model as IEnumerable<FaqCategory>;

			Assert.NotNull(model);
			Assert.True(!model.Any());
		}

		[Fact]
		public void TestFilledFaqRepository()
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

			IFaqRepository repo = new EFFaqRepository(context);
			IFaqManager manager = new FaqManager(repo);

			var target = new FaqController(manager);

			var result = target.Index() as ViewResult;
			var model = result.Model as IEnumerable<FaqCategory>;

			Assert.NotNull(model);
			Assert.True(model.Count() == 3);
		}
	}
}
