using HomeMyDay.Models;
using HomeMyDay.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
	        if (_vacancieRepository.Vacancies.Any())
	        {
		        return View(_vacancieRepository.Vacancies.OrderByDescending(a => a.Id));
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

            return View(vacancie);
        }
    }
}
