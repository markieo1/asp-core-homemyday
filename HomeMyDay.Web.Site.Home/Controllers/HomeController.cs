using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
			return View();
        }
    }
}
