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
    public class SupriseViewComponentTest
    {

		[Fact]
		public void TestNoSuprise()
		{
			Page suprise = null;

			var repo = new Mock<IPageRepository>();
			repo.Setup(r=>r.GetSuprise()).Returns(suprise);
			SuprisePopUpViewComponent target = new SuprisePopUpViewComponent(repo.Object);

			var result = target.Invoke() as ViewViewComponentResult;

			Assert.Equal("NoSuprise", result.ViewName);

		}

		[Fact]
		public void TestSupriseNotNullAndEmpty()
		{
			Page suprise = new Page {Page_Id = "TheSuprise", Title = "Hallo", Content="Test" };

			var repo = new Mock<IPageRepository>();
			repo.Setup(r => r.GetSuprise()).Returns(suprise);

			SuprisePopUpViewComponent target = new SuprisePopUpViewComponent(repo.Object);

			SuprisePopUpViewModel mo = (SuprisePopUpViewModel)(target.Invoke() as ViewViewComponentResult).ViewData.Model;

			Assert.NotNull(mo);
			Assert.NotEmpty(mo.Title);
			Assert.NotEmpty(mo.Content);
			Assert.NotNull(mo.Title);
			Assert.NotNull(mo.Content);
			

		}
	}
}
