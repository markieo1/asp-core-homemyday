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
			Page suprise = new Page(){Id = 1, Page_Name = "TheSurpriseModified", Title = "Hallo123", Content = "Test321" };	

			var repo = new Mock<IPageRepository>();		
			repo.Setup(x => x.GetPage(1)).Returns(suprise);
			repo.Setup(x => x.EditPage(1, suprise));

			var manager = new PageManager(repo.Object);							  

			// Arrange - create a controller
			PagesController target = new PagesController(manager);

			// Modify page
			suprise.Title = "Testing";

			// Action	 
			var model = (target.Edit(1) as ViewResult).ViewData.Model as PageViewModel;

			// Assert				   
			Assert.NotNull(model);
			Assert.Equal("Testing", model.Title);
		}
	}
}
