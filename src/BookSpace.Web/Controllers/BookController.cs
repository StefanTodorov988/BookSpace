using System;
using AutoMapper;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Models.BookViewModels;
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

        [HttpGet("/BookDetails/{bookid}")]
        public IActionResult BookDetails(string bookId)
        {
            //TODO:Not finished
            var dbModel = this.bookRepository.GetByIdAsync(bookId).Result;
            //var dbGenre = this.bookRepository.GetBookGenresAsync(bookId).Result;
            var mappedBookViewModel = this.objectMapper.Map<DetailedBookViewModel>(dbModel);

            return View(mappedBookViewModel);
        }

                public IActionResult GetBookGenres(string bookId)
        {
            //TODO:Not finished
            var dbModel = this.bookRepository.GetBookGenresAsync(bookId);
            var mappedGenreViewModel = this.objectMapper.Map<GenreViewModel>(dbModel);

            return View();
        }

        public IActionResult BooksByAuthor(string bookId)
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