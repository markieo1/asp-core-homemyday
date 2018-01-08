using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class WarrantyController : Controller
    {
	    [HttpGet]
	    public ViewResult Index()
	    {
		    return View();
	    }				  
    }
}
