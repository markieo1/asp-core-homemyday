using HomeMyDay.Database.Identity;
using HomeMyDay.Helpers;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Controllers.Cms
{
    [Area("CMS")]
    [Authorize(Policy = IdentityPolicies.Administrator)]
    public class VacancyController : Controller
    {
        private readonly IVacancyRepository _vancancyRepository;

        public VacancyController(IVacancyRepository repository)
        {
            _vancancyRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page, int? pageSize)
        {
            PaginatedList<Vacancy> paginatedResult = await _vancancyRepository.List(page ?? 1, pageSize ?? 5);
            return View(paginatedResult);
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(VacancyViewModel model)
        {
            if (ModelState.IsValid) {
                _vancancyRepository.SaveVacancy(model.JobTitle, model.Company, model.City, model.AboutVacancy, model.AboutFunction, model.JobRequirements, model.WeOffer);
                return RedirectToAction(nameof(Index));
            }
            else {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            _vancancyRepository.DeleteVacancy(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ViewResult Edit(long id)
        {
            return View(_vancancyRepository.GetVacancy(id));
        }

        [HttpPost]
        public IActionResult Edit(Vacancy vacancy)
        {
            if(ModelState.IsValid)
            {
                _vancancyRepository.UpdateVacancy(vacancy);
                TempData["Message"] = $"{vacancy.JobTitle} has been saved";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
    }
}
