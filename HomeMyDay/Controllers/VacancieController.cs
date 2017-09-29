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
            return View(_vacancieRepository.GetVacancies.OrderByDescending(a => a.Id));                
        }

        [HttpGet]
        public IActionResult Detail(long id)
        {
            Vacancie Vacancie = null;

            try
            {
                Vacancie = _vacancieRepository.GetVacancie(id);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return View(Vacancie);
        }
    }
}
