using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Controllers
{
	public class NewspaperController : Controller
	{
		private readonly INewspaperRepository _newspaperRepository;

		public NewspaperController(INewspaperRepository newspaperRepository)
		{
			_newspaperRepository = newspaperRepository;
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
				if (_newspaperRepository.Subscribe(newspaperViewModel.Email))
				{
					return View("Result");
				}
			}

			ModelState.AddModelError("Error", "Het ingevulde emailadres is niet correct of is al gebruikt.");
			return View();
		}
	}
}
