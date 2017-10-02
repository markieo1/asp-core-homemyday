using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Controllers
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
