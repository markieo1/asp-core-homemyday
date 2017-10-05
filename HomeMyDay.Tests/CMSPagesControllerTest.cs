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
		public void EditSucces()
		{

			// Arrange - create the mock repository
			Page suprise = new Page { Page_Name = "TheSuprise", Title = "Hallo", Content = "Test" };

			var repo = new Mock<IPageRepository>();
			repo.Setup(r => r.GetPage(1)).Returns(suprise);

			// Arrange - create a controller
			PagesController target = new PagesController(repo.Object);

			// Action
			var mo = (target.Edit(1, suprise) as ViewResult).ViewData.Model;

			// Assert
			//Check if edit was called
			repo.Verify(foo => foo.EditPage(1, suprise));
			Assert.NotNull(mo);
		}
	}
}
