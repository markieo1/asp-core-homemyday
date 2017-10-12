using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Controllers
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
