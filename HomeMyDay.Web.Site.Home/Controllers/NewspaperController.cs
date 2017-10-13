using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Web.Base.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class NewspaperController : Controller
	{
		private readonly INewspaperManager _newspaperManager;

		public NewspaperController(INewspaperManager newspaperManager)
		{
			_newspaperManager = newspaperManager;
		}

		[HttpGet]
		public ViewResult Index()
		{
			return View();
		}

		[HttpPost]
		public ViewResult Index(NewspaperViewModel newspaperViewModel)
		{
			if (ModelState.IsValid)
			{
				if (_newspaperManager.Subscribe(newspaperViewModel.Email))
				{
					return View("Result");
				}
			}

			ModelState.AddModelError("Error", "Het ingevulde emailadres is niet correct of is al gebruikt.");
			return View();
		}
	}
}
