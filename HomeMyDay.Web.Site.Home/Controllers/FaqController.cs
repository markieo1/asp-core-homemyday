using HomeMyDay.Web.Base.Managers;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class FaqController : Controller
	{
		private readonly IFaqManager _faqManager;	

		public FaqController(IFaqManager faqManager)
		{
			_faqManager = faqManager;
		} 

        public IActionResult Index()
        {
			return View(_faqManager.GetFaqCategories());
        }
    }
}