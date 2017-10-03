using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeMyDay.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _repository ;

        public ReviewController(IReviewRepository repo)
        {
            _repository = repo;           
        }

        public ViewResult Index()
        {
            var reviews = _repository.Reviews;
            return View(reviews);
        }
    }
}