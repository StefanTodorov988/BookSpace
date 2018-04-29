using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookSpace.Models;
using BookSpace.Repositories;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Areas.Admin.Models.ApplicationUserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSpace.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IApplicationUserRepository repository;
        private readonly IMapper objectMapper;

        public AdminController(IApplicationUserRepository repository, IMapper objectMapper)
        {
            this.repository = repository;
            this.objectMapper = objectMapper;
        }

        public IActionResult AllUsers()
        {
            var users = this.repository.GetAllAsync().Result.ToList();
            
            var userViewModels = new List<ApplicationUserViewModel>();

            foreach (var user in users)
            {
               userViewModels.Add(this.objectMapper.Map<ApplicationUserViewModel>(user));
            }


            return View(userViewModels);
        }

        public IActionResult EditUser(string id)
        {
            var user = this.repository.GetByIdAsync(id).Result;
            var userViewModel = objectMapper.Map<ApplicationUserViewModel>(user);

            return this.PartialView("_EditUser", userViewModel);
        }

        //public IActionResult AllBooks()
        //{
        //    var users = this.repository.GetAllAsync().Result.ToList();

        //    var mappedUsers = new List<ApplicationUserViewModel>();

        //    foreach (var user in users)
        //    {
        //        mappedUsers.Add(this.objectMapper.Map<ApplicationUserViewModel>(user));
        //    }


        //    return View(mappedUsers);
        //}

        public IActionResult Index()
        {
            
            return View();
        }
    }
}