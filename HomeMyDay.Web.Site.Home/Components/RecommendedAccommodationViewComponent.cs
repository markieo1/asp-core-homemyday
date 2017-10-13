using HomeMyDay.Web.Base.Managers;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Home.Components
{
    public class RecommendedAccommodationViewComponent : ViewComponent
    {
        private readonly IAccommodationManager _accommodationManager;

        public RecommendedAccommodationViewComponent(IAccommodationManager accommodationManager)
        {
	        _accommodationManager = accommodationManager;
        }

        public IViewComponentResult Invoke()
        {																	 
            return View(_accommodationManager.GetRecommendedAccommodations());
        }
    }
}
