using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookSpace.Models;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Models.BookViewModels;
using BookSpace.Web.Models.GenreViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookSpace.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper objectMapper;
        public SearchController(IBookRepository bookRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.objectMapper = mapper;
        }
        public async Task<IActionResult> Index(string filter)
        {

            var searchedBooks = await bookRepository.Search(x => x.Title.Contains(filter) || x.Author.Contains(filter));

            var searchedBookViewModels = this.objectMapper.Map<IEnumerable<Book>, IEnumerable<SearchedBookViewModel>>(searchedBooks);

            return View(searchedBookViewModels);
        }
    }
}