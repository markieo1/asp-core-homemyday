using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Controllers
{
	public class NewspaperController : Controller
    {
	    public ViewResult Index()
	    {
		    return View();
	    }

	    public ViewResult Subscribe(NewspaperViewModel newspaperViewModel)
	    {
		    return null;
	    }
    }
}
