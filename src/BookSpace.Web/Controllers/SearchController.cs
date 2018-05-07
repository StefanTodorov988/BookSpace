using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookSpace.Models;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Models.BookViewModels;
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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchBook(string filter,string filerRadio = "default")
        {
            List<Book> foundBooks = new List<Book>();
            if (filerRadio == "default")
            {
                foundBooks = new List<Book>(await bookRepository.Search(x => x.Title.Contains(filter) || x.Author.Contains(filter)));
            }
            else if (filerRadio == "title")
            {
                foundBooks = new List<Book>(await bookRepository.Search(x => x.Title.Contains(filter)));
            }
            else if (filerRadio == "author")
            {
                foundBooks = new List<Book>(await bookRepository.Search(x => x.Author.Contains(filter)));
            }
            else if (filerRadio == "genre")
            {
                foundBooks = new List<Book>(await this.SerachByGenre(filter));
            }
            else
            {
                foundBooks = new List<Book>(await this.SerachByTag(filter));
            }

            var foundBooksViewModel = this.objectMapper.Map<IEnumerable<Book>, IEnumerable<SearchedBookViewModel>>(foundBooks);

            return View("/Views/Shared/Book/_BookSearchResultPagePartial.cshtml", foundBooksViewModel);
        }

        public async Task<IEnumerable<Book>> SerachByGenre(string filter)
        {

            var foundBooks = await bookRepository.SearchByNavigationProperty
                                    ("BookGenre", "Genre", b => CheckBookGenres(b, filter));
            return foundBooks;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> SerachByTag(string filter)
        {

            var foundBooks = await bookRepository.SearchByNavigationProperty
                                   ("BookTag", "Tag", b => CheckBookTags(b, filter));

            return foundBooks;
        }

        #region Helpers
        private bool CheckBookGenres(Book book, string filter)
        {
            var enumerator = book.BookGenres.GetEnumerator();
            while (enumerator.Current != null)
            {
                if (enumerator.Current.Genre.Name.Contains(filter))
                    return true;
            };

            return false;
        }

        private bool CheckBookTags(Book book, string filter)
        {
            var enumerator = book.BookGenres.GetEnumerator();
            while (enumerator.Current != null)
            {
                if (enumerator.Current.Genre.Name.Contains(filter))
                    return true;
            };

            return false;
        }
        #endregion
    }
}