using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Repositories;
using BookSpace.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSpace.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationUserRepository repository;

        public AdminController(ApplicationUserRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult AdminIndex()
        {
            return View();
        }

        public IActionResult EditUsers()
        {
            var users =  this.repository.GetAllAsync();

            return View();
        }
    }
}