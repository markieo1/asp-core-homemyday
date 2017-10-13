using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Web.Base.Managers.Implementation;
using HomeMyDay.Web.Base.ViewModels;
using HomeMyDay.Web.Site.Cms.Controllers;
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

			// Arrange - create the mock repo and manager
			PageViewModel surprisePageViewModel = new PageViewModel() {Page_Name = "TheSurprise", Title = "Hallo", Content = "Test" };
			Page suprise = new Page(){Id = 1, Page_Name = "TheSurpriseModified", Title = "Hallo123", Content = "Test321" };	

			var repo = new Mock<IPageRepository>();
			repo.SetupGet(x => x.GetPage(1)).Returns(suprise);	

			var manager = new PageManager(repo.Object);							  

			// Arrange - create a controller
			PagesController target = new PagesController(manager);

			// Action
			var model = (target.Edit(1) as ViewResult).ViewData.Model;

			// Assert
			//Check if edit was called
			repo.Verify(p => p.EditPage(1, suprise));
		}
	}
}
