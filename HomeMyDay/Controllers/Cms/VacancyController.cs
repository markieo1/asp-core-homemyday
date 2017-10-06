using HomeMyDay.Database.Identity;
using HomeMyDay.Helpers;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
