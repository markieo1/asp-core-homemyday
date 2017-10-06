using System.Threading.Tasks;
using HomeMyDay.Database.Identity;
using HomeMyDay.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Models;
using System.Linq;

namespace HomeMyDay.Controllers.Cms
{
	[Area("CMS")]
	//[Authorize(Policy = IdentityPolicies.Administrator)]
	public class FaqCmsController : Controller
	{
		private readonly IFaqRepository _faqRepository;

		public FaqCmsController(IFaqRepository repository)
		{
			_faqRepository = repository;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int? page, int? pageSize)
		{
			var paginatedResult = await _faqRepository.List(page ?? 1, pageSize ?? 5);
			return View(paginatedResult);
		}

		[HttpGet]
		public IActionResult EditCategory(long id)
		{
			return View(_faqRepository.Categories.FirstOrDefault(r=>r.Id == id));
		}

		[HttpPost]
		public IActionResult EditCategory(FaqCategory cat)
		{
			if (ModelState.IsValid)
			{
				_faqRepository.SaveFaqCategory(cat);
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Error, something went wrong while editing");
				return View();
			}
		}

		public IActionResult AddCategory() => View("Edit", new FaqCategory());

		public IActionResult DeleteCategory(long id)
		{
			FaqCategory deletedCat = _faqRepository.DeleteFaqCategory(id);
			if (deletedCat != null)
			{
				TempData["message"] = $"{deletedCat.CategoryName} was deleted";
			}
			return RedirectToAction("Index");
		}

	}
}
