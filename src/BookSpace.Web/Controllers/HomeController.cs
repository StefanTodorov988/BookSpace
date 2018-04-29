using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Factories;
using BookSpace.Models;
using Microsoft.AspNetCore.Mvc;
using BookSpace.Web.Models;

namespace BookSpace.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookFactory bookFactory;

        public HomeController(IBookFactory bookFactory)
        {
            this.bookFactory = bookFactory;
        }
        public IActionResult Index()
        {
            Book test = bookFactory.Create("chika", "loca", "NO BUENO");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Book()
        {
          
            return View();
        }

        public IActionResult Category()
        {

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
