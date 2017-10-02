using HomeMyDay.Models;
using HomeMyDay.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using HomeMyDay.ViewModels;

namespace HomeMyDay.Controllers
{
	public class VacancieController : Controller
	{
		private readonly IVacancieRepository _vacancieRepository;

		public VacancieController(IVacancieRepository repo)
		{
			_vacancieRepository = repo;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var vacancies = _vacancieRepository.Vacancies.ToList();
			var vacancieViewModels = new List<VacancieViewModel>();
			vacancies.ForEach(x => vacancieViewModels.Add(VacancieViewModel.FromVacancie(x)));

			if (vacancieViewModels.Any())
			{
				return View(vacancieViewModels);
			}

			return View("NoVacancies");
		}

		[HttpGet]
		public IActionResult Detail(long id)
		{
			Vacancie vacancie = null;

			try
			{
				vacancie = _vacancieRepository.GetVacancie(id);
			}
			catch (ArgumentOutOfRangeException)
			{
				return BadRequest();
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}

			var vacancieViewModel = VacancieViewModel.FromVacancie(vacancie);
			return View(vacancieViewModel);
		}
	}
}
