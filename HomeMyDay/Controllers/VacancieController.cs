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
            if(!_vacancieRepository.Vacancies.Any())
            {
                return View("../Partials/NoVacancies.cshtml");
            }
            else
            {
                return View(_vacancieRepository.Vacancies.OrderByDescending(a => a.Id));
            }
                          
        }

        [HttpGet]
        public IActionResult Detail(long id)
        {
            Vacancie vacancie = null;

            try
            {
                vacancie = _vacancieRepository.Vacancie(id);
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
