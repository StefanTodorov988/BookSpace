using AutoMapper;
using BookSpace.Models;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Models.BookViewModels;
using BookSpace.Web.Models.CommentsViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper objectMapper;
        private const int recordsOnPage = 30;

        public BookController(IBookRepository bookRepository, IMapper objectMapper)
        {
            this.bookRepository = bookRepository;
            this.objectMapper = objectMapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await this.GetBooksPage(page));
        }

        public IActionResult Category([FromRoute] string id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BooksList([FromQuery] int page)
        {
            return PartialView("Book/_BooksPagePartial", await this.GetBooksPage(page));
        }

        public async Task<IActionResult> BookDetails([FromRoute] string id)
        {
            var book = await this.bookRepository.GetByIdAsync(id);
            var comments = await this.bookRepository.GetBookCommentsAsync(id);
            var genres = await this.bookRepository.GetBookGenresAsync(id);
            var tags = await this.bookRepository.GetBookTagsAsync(id);


            var bookViewModel = this.objectMapper.Map<Book, BookViewModel>(book);
            var commentsViewModel = this.objectMapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);
            var propertiesViewModel = new BookPropertiesViewModel();
            propertiesViewModel.Comments = commentsViewModel ;
            propertiesViewModel.Tags = tags.ToList().Select(t => t.Value);
            propertiesViewModel.Genres = genres.ToList().Select(g => g.Name);

            var singleBookViewModel = new SingleBookViewModel { Book = bookViewModel, Properties = propertiesViewModel };
            return View(singleBookViewModel);
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

        #region Helpers

        private async Task<IEnumerable<BooksIndexViewModel>> GetBooksPage(int page)
        {
            var books = await this.bookRepository.GetPaged(page, recordsOnPage);
            var booksViewModels = this.objectMapper.Map<IEnumerable<Book>, IEnumerable<BooksIndexViewModel>>(books.Results);
            return booksViewModels;
        }
        #endregion
    }
}