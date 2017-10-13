using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Web.Base.ViewModels;
using HomeMyDay.Web.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using Xunit;

namespace HomeMyDay.Web.Site.Home.Tests
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
