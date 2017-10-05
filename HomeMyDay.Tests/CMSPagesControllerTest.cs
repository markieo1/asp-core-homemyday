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
			Page suprise = new Page { Page_Id = "TheSuprise", Title = "Hallo", Content = "Test" };

			var repo = new Mock<IPageRepository>();
			repo.Setup(r => r.GetPage("TheSuprise")).Returns(suprise);

			// Arrange - create a controller
			PagesController target = new PagesController(repo.Object);

			// Action
			var mo = (target.Edit("TheSuprise", suprise) as ViewResult).ViewData.Model;

			// Assert
			//Check if edit was called
			repo.Verify(foo => foo.EditPage("TheSuprise", suprise));
			Assert.NotNull(mo);
		}
	}
}
