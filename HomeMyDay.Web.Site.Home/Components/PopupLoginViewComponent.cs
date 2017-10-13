using HomeMyDay.Web.Base.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Home.Components
{
	public class PopupLoginViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			LoginViewModel model = new LoginViewModel();
			return View(model);
		}
	}
}
