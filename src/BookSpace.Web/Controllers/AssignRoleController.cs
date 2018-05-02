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
        private readonly IServiceProvider serviceProvider;

        public AssignRoleController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
       
        public IActionResult Index()
        {
            return View();
        }

    }
}