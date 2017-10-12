using HomeMyDay.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HomeMyDay.Web.Home.Controllers
{
    public class FaqController : Controller
    {
        private readonly IFaqRepository _faqRepository;

        public FaqController(IFaqRepository repository)
        {
            _faqRepository = repository;
        }

        public IActionResult Index()
        {
            var faq = _faqRepository.GetCategoriesAndQuestions();
            return View(faq);
        }
    }
}