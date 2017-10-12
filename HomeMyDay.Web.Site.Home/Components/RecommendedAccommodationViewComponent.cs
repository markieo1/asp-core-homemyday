using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Components
{
    public class RecommendedAccommodationViewComponent : ViewComponent
    {
        private IAccommodationRepository repository;

        public RecommendedAccommodationViewComponent(IAccommodationRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var model = repository.GetRecommendedAccommodations();

            return View(model);
        }
    }
}
