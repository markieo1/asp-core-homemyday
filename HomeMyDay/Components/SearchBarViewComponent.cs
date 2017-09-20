using HomeMyDay.Models;
using HomeMyDay.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Components
{
	public class SearchBarViewComponent : ViewComponent
	{
		private readonly IHolidayRepository _holidayRepository;

		public SearchBarViewComponent(IHolidayRepository repository)
		{
			_holidayRepository = repository;
		}

		public IViewComponentResult Invoke()
		{
			IEnumerable<Accommodation> accommodations = _holidayRepository.GetAccommodations();

			return View(accommodations);
		}
	}
}
