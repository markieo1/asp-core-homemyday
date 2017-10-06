using System.Threading.Tasks;
using HomeMyDay.Database.Identity;
using HomeMyDay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
			var paginatedResult = await _faqRepository.List(page ?? 1, pageSize ?? 5);
			return View(paginatedResult);
		} 
	}
}
