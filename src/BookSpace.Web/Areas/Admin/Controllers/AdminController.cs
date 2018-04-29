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

        public IActionResult AdminIndex()
        {
            var users = this.repository.GetAllAsync().Result.ToList();
            
            var mappedUsers = new List<ApplicationUserViewModel>();

            foreach (var user in users)
            {
               mappedUsers.Add(this.objectMapper.Map<ApplicationUserViewModel>(user));
            }


            return View(mappedUsers);
        }

        //public IActionResult EditUsers()
        //{
        //    var users =  this.repository.GetAllAsync();

        //    return View();
        //}
    }
}