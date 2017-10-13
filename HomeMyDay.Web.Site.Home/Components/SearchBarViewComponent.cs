using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Web.Base.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Home.Components
{
	public class SearchBarViewComponent : ViewComponent
	{
		private readonly IAccommodationManager _accommodationManager;

		public SearchBarViewComponent(IAccommodationManager accommodationManager)
		{
			_accommodationManager = accommodationManager;
		}

		public IViewComponentResult Invoke()
		{	  
			var accommodations = _accommodationManager.GetAccommodations();

			var viewModel = new AccommodationSearchViewModel
			{
				Accommodations = accommodations
			};

			return View(viewModel);
		}
	}
}
