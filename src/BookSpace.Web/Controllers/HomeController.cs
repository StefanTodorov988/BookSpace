using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Factories;
using BookSpace.Models;
using Microsoft.AspNetCore.Mvc;
using BookSpace.Web.Models;
using BookSpace.Repositories.Contracts;

namespace BookSpace.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository bookRepository;

        public HomeController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public IActionResult Index()
        {
            var bookComments = this.bookRepository
               .GetBookCommentsAsync("0053235A-26E8-4335-9E9E-4E75936A9639").GetAwaiter().GetResult();
            var bookGenres = this.bookRepository
               .GetBookGenresAsync("0053235A-26E8-4335-9E9E-4E75936A9639").GetAwaiter().GetResult();
            var bookTags = this.bookRepository
               .GetBookTagsAsync("0053235A-26E8-4335-9E9E-4E75936A9639").GetAwaiter().GetResult();
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
