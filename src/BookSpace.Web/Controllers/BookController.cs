using AutoMapper;
using BookSpace.Models;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Models.BookViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            var books = await this.bookRepository.GetPaged(1, 30);
            var booksViewModels = this.objectMapper.Map<IEnumerable<Book>, IEnumerable<BooksIndexViewModel>>(books.Results);
            return View(booksViewModels);
        }

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