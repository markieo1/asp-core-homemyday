using HomeMyDay.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Home.Controllers
{
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
			return View();
        }
    }
}
