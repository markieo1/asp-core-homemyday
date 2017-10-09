using HomeMyDay.Components;
using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
    public class SurpriseViewComponentTest
    {

		[Fact]
		public void TestNoSurprise()
		{
			Page surprise = null;

			var repo = new Mock<IPageRepository>();
			repo.Setup(r=>r.GetPage(1)).Returns(surprise);
			SurprisePopUpViewComponent target = new SurprisePopUpViewComponent(repo.Object);

			var result = target.Invoke() as ViewViewComponentResult;

			Assert.Equal("NoSurprise", result.ViewName);

		}

		[Fact]
		public void TestSurpriseNotNullAndEmpty()
		{
			Page surprise = new Page {Page_Name = "TheSurprise", Title = "Hallo", Content="Test" };

			var repo = new Mock<IPageRepository>();
			repo.Setup(r => r.GetPage(1)).Returns(surprise);

			SurprisePopUpViewComponent target = new SurprisePopUpViewComponent(repo.Object);

			PageViewModel mo = (PageViewModel)(target.Invoke() as ViewViewComponentResult).ViewData.Model;

			Assert.NotNull(mo);
			Assert.NotEmpty(mo.Title);
			Assert.NotEmpty(mo.Content);
			Assert.NotNull(mo.Title);
			Assert.NotNull(mo.Content);
			

		}
	}
}
