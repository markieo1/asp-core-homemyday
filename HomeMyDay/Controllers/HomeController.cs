using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Controllers
{
	public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
