using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Controllers
{
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
			return View();
        }
    }
}
