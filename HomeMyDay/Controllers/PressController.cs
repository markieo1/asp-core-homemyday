﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Controllers
{
    public class PressController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}