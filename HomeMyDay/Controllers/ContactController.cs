using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Home.Controllers
{
	public class ContactController : Controller
    {							
        public IActionResult Index()
        {
            return View();
        }
    }
}
