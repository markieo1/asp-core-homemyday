using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using HomeMyDay.Web.Base.Managers;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class VacancyController : Controller
	{
		private readonly IVacancyManager _vacancyManager;

		public VacancyController(IVacancyManager vacancyManager)
		{
			_vacancyManager = vacancyManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var vacancies = _vacancyManager.GetVacancies();
			if (vacancies.Any())
			{
				return View(vacancies);
			}

			return View("NoVacancies");	  
		}

		[HttpGet]
		public IActionResult Detail(long id)
		{							   
			try
			{
				return View(_vacancyManager.GetVacancy(id));   
			}	 
			catch (KeyNotFoundException)
			{
				return NotFound();
			}					  
		}
	}
}
