using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Site.Home.Controllers
{
    public class InfoController: Controller
    {
	    public IActionResult Index()
	    {
		    return View();
	    }
	}
}
