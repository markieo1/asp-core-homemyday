using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Controllers
{
	public class ContactController : Controller
    {							
        public IActionResult Index()
        {
            return View();
        }
    }
}
