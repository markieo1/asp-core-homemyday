using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Home.Controllers
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
