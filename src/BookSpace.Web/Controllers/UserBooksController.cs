﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookSpace.Web.Controllers
{
    public class UserBooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}