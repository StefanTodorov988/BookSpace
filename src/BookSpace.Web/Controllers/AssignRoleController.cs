using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSpace.Web.Controllers
{
    public class AssignRoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            var role = new IdentityRole();

            return View(role);
        }

        //[HttpPost]
        //public IActionResult Create(IdentityRole role)
        //{
        //    context.Roles.Add(role);
        //}
    }
}