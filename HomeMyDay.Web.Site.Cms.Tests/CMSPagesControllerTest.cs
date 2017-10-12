using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Web.Controllers.Cms;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HomeMyDay.Web.Site.Cms.Tests
{
	public class CMSPagesControllerTest
	{
		[Fact]
		public void TestEditPageIsCalled()
		{

			// Arrange - create the mock repository
			Page surprise = new Page {Id =1, Page_Name = "TheSurprise", Title = "Hallo", Content = "Test" };

			var repo = new Mock<IPageRepository>();
			repo.Setup(r => r.GetPage(1)).Returns(surprise);

			// Arrange - create a controller
			PagesController target = new PagesController(repo.Object);

			// Action
			var model = (target.Edit(1, surprise) as ViewResult).ViewData.Model;

			// Assert
			//Check if edit was called
			repo.Verify(p => p.EditPage(1, surprise));
		}
	}
}
