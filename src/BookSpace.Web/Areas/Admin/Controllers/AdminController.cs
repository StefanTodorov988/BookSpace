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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSpace.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IApplicationUserRepository repository;
        private readonly IMapper objectMapper;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(IApplicationUserRepository repository, IMapper objectMapper, UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.objectMapper = objectMapper;
            this.userManager = userManager;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(ApplicationUserViewModel userViewModel)
        {
            var dbModel = this.repository.GetByIdAsync(userViewModel.Id).Result;

            var user = this.objectMapper.Map(userViewModel, dbModel);


            if (userViewModel.isAdmin)
            {
                await this.userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                await this.userManager.RemoveFromRoleAsync(user, "Admin");
            }

            await this.repository.UpdateAsync(user);
            //await this.userManager.UpdateAsync(user);

            return this.RedirectToAction("AllUsers");
        }


        public IActionResult EditUser(string id)
        {
            var user = this.repository.GetByIdAsync(id).Result;
            var userViewModel = objectMapper.Map<ApplicationUserViewModel>(user);

            return View(userViewModel);
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