using HomeMyDay.Core.Repository;
using HomeMyDay.Web.Base.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class FaqController : FaqBaseController
	{								
	    public FaqController(IFaqRepository repository)
			:base(repository)
	    {						

	    } 

        public IActionResult Index()
        {
			return View(GetFaqCategories());
        }
    }
}