using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Core.Extensions;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Controllers.Cms
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
        public IActionResult Add(Vacancy model)
        {
            if (ModelState.IsValid) {
                _vancancyRepository.SaveVacancy(model);
                return RedirectToAction(nameof(Index));
            }
            else {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            await _vancancyRepository.DeleteVacancy(id);

            return RedirectToAction(
                        actionName: nameof(Index),
                        controllerName: nameof(VacancyController).TrimControllerName());
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            Vacancy vacancy;

            try
            {
                if (id <= 0)
                {
                    vacancy = new Vacancy();
                }
                else
                {
                    vacancy = _vancancyRepository.GetVacancy(id);
                }
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (ArgumentOutOfRangeException)
            {
                return new BadRequestResult();
            }

            return View(vacancy);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Vacancy vacancy)
        {
            if(ModelState.IsValid)
            {
                await _vancancyRepository.SaveVacancy(vacancy);
                return RedirectToAction(
                    actionName: nameof(Index),
                    controllerName: nameof(VacancyController).TrimControllerName());
            }
            else
            {
                return View(vacancy);
            }
        }
    }
}
