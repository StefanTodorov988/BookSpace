using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Areas.Book.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookSpace.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper objectMapper;

        public BookController(IBookRepository bookRepository, IMapper objectMapper)
        {
            this.bookRepository = bookRepository;
            this.objectMapper = objectMapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BookDetails(string bookId)
        {
            var dbModel = this.bookRepository.GetByIdAsync(bookId).Result;
            var mappedBookViewModel = this.objectMapper.Map<DetailedBookViewModel>(dbModel);

            return View(mappedBookViewModel);
        }


        public IActionResult BooksByAuthor()
        {
            return View();
        }

        public IActionResult BooksByGenre()
        {
            return View();
        }

        public IActionResult BooksByTag()
        {
            return View();
        }

        public IActionResult BooksByTitle()
        {
            return View();
        }
    }
}