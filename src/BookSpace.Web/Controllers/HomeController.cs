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
using BookSpace.Web.Models.BookViewModels;
using AutoMapper;

namespace BookSpace.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper objectMapper;

        public HomeController(IBookRepository bookRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.objectMapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var books = await this.bookRepository.FindByExpressionOrdered(book => book.Rating, 12);
            var booksViewModels = this.objectMapper.Map<IEnumerable<Book>, IEnumerable<PopularBookViewModel>>(books);

            return View(booksViewModels);
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
