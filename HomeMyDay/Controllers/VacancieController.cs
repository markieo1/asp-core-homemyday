using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

        public ViewResult Index()
        {
            return View(_vacancieRepository.GetVacancies.OrderByDescending(a => a.Id));                
        }

        public ViewResult Detail(int id)
        {
            var getVacancie = _vacancieRepository.GetVacancie(id);
            return View(getVacancie);
        }
    }
}
