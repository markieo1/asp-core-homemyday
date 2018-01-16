using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class HomeController : Controller
    {
        private readonly IConfiguration _iconfiguration;
        public HomeController(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public IActionResult Index()
        {
            ViewData["url"] = _iconfiguration.GetSection("ExternalAddresses").GetSection("SPA-IP").Value;
            return View();
        }
    }
}
