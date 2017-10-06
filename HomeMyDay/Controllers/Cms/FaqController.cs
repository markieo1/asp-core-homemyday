using System.Threading.Tasks;
using HomeMyDay.Database.Identity;
using HomeMyDay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Models;
using HomeMyDay.Helpers;
using HomeMyDay.ViewModels;

namespace HomeMyDay.Controllers.Cms
{
	[Area("CMS")]
	[Authorize(Policy = IdentityPolicies.Administrator)]
	public class FaqController : Controller
	{
		private readonly IFaqRepository _faqRepository;

		public FaqController(IFaqRepository repository)
		{
			_faqRepository = repository;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int? page, int? pageSize)
		{
			var paginatedResult = await _faqRepository.ListCategories(page ?? 1, pageSize ?? 5);
			return View(paginatedResult);
		}

		[HttpGet]
		public async Task<IActionResult> Questions(long id, int? page, int? pageSize)
		{
			FaqCategory category = _faqRepository.GetCategory(id);
			PaginatedList<FaqQuestion> paginatedResult = await _faqRepository.ListQuestions(id, page ?? 1, pageSize ?? 5);

			FaqQuestionsViewModel viewModel = new FaqQuestionsViewModel()
			{
				Category = category,
				Questions = paginatedResult
			};

			return View(viewModel);
		}
	}
}
