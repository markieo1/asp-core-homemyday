using HomeMyDay.Controllers.Cms;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HomeMyDay.Tests
{
	public class CMSPagesControllerTest
	{
		[Fact]
		public void EditSuccess()
		{

			// Arrange - create the mock repository
			Page surprise = new Page {Id =1, Page_Name = "TheSurprise", Title = "Hallo", Content = "Test" };

			var repo = new Mock<IPageRepository>();
			repo.Setup(r => r.GetPage(1)).Returns(surprise);

			// Arrange - create a controller
			PagesController target = new PagesController(repo.Object);
			Page newsurp = new Page() {Title = "Test", Content = "New" };

			// Action
			var mo = (target.Edit(1, newsurp) as ViewResult).ViewData.Model;

			// Assert
			//Check if edit was called
			repo.Verify(foo => foo.EditPage(1, newsurp));
			Assert.NotNull(mo);
		}
	}
}
