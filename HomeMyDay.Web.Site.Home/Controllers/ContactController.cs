using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class ContactController : Controller
    {							
        public IActionResult Index()
        {
            return View();
        }
    }
}
