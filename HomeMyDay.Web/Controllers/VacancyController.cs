using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using HomeMyDay.Web.ViewModels;

namespace HomeMyDay.Web.Controllers
{
	public class VacancyController : Controller
	{
		private readonly IVacancyRepository _vacancieRepository;

		public VacancyController(IVacancyRepository repo)
		{
			_vacancieRepository = repo;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var vacancies = _vacancieRepository.Vacancies;
            
			if (vacancies.Any())
			{
				return View(vacancies);
			}

			return View("NoVacancies");
		}

		[HttpGet]
		public IActionResult Detail(long id)
		{
			Vacancy vacancie = null;

			try
			{
				vacancie = _vacancieRepository.GetVacancy(id);
			}
			catch (ArgumentOutOfRangeException)
			{
				return BadRequest();
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}

			return View(vacancie);
		}
	}
}
