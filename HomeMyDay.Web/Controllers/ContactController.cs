using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Controllers
{
	public class ContactController : Controller
    {							
        public IActionResult Index()
        {
            return View();
        }
    }
}
