using AutoMapper;
using BookSpace.Models;
using BookSpace.Repositories;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Models.BookViewModels;
using BookSpace.Web.Models.CommentsViewModel;
using BookSpace.Web.Models.GenreViewModels;
using Microsoft.AspNetCore.Identity;
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
        private readonly IGenreRepository genreRepository;
        private readonly IBookUserRepository bookUserRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private const int recordsOnPageIndex = 30;
        private const int recordsOnPageCategory = 10;

        public BookController(IBookRepository bookRepository,
                              IGenreRepository genreRepository,
                              IBookUserRepository bookUserRepository,
                              UserManager<ApplicationUser> userManager,
                              IMapper objectMapper)
        {
            this.bookRepository = bookRepository;
            this.genreRepository = genreRepository;
            this.bookUserRepository = bookUserRepository;
            this._userManager = userManager;
            this.objectMapper = objectMapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await this.GetBooksPage(page));
        }

        public async Task<IActionResult> Category([FromRoute] string id, int page = 1)
        {
            var genre = await this.genreRepository.GetByIdAsync(id);
            var genreViewModel = this.objectMapper.Map<Genre, GenreViewModel>(genre);
            var books = await this.GetBooksByCategoryPage(id, page);
            var categoryViewModel = new CategoryPageViewModel { Genre = genreViewModel, Books = books };

            return View(categoryViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> BooksList([FromQuery] int page)
        {
            return PartialView("Book/_BooksPagePartial", await this.GetBooksPage(page));
        }

        [HttpGet]
        public async Task<IActionResult> BooksByCategoryList([FromRoute] string id,[FromQuery] int page)
        {
            var genre = await this.genreRepository.GetByIdAsync(id);
            var genreViewModel = this.objectMapper.Map<Genre, GenreViewModel>(genre);
            var books = await this.GetBooksByCategoryPage(id, page);
            var categoryViewModel = new CategoryPageViewModel { Genre = genreViewModel, Books = books };

            return PartialView("Book/_BookByCategoryPagePartial", categoryViewModel);
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<IActionResult> BookDetails([FromRoute] string id)
        {
            var book = await this.bookRepository.GetByIdAsync(id);
            var comments = await this.bookRepository.GetBookCommentsAsync(id);
            var genres = await this.bookRepository.GetBookGenresAsync(id);
            var tags = await this.bookRepository.GetBookTagsAsync(id);
            var bookUser = await this.bookUserRepository.GetAsync(bu => bu.BookId == id);

            var bookViewModel = this.objectMapper.Map<Book, BookViewModel>(book);
            var commentsViewModel = this.objectMapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);
            var propertiesViewModel = new BookPropertiesViewModel();
            propertiesViewModel.Comments = commentsViewModel ;
            propertiesViewModel.Tags = tags.ToList().Select(t => t.Value);
            propertiesViewModel.Genres = genres.ToList().Select(g => g.Name);
            bool isRated = bookUser == null || bookUser.HasRatedBook == false ? false : true; 
            int userRating = bookUser == null || bookUser.HasRatedBook == false ? 0 : bookUser.Rate;

            var singleBookViewModel = new SingleBookViewModel
            {
                Book = bookViewModel,
                Properties = propertiesViewModel,
                IsRated = isRated,
                UserRating = userRating
            };
            return View(singleBookViewModel);
        }

        public async Task<IActionResult> UpdateBookRating(string id, string rate, bool isNewUser)
        {
            var book = await this.bookRepository.GetByIdAsync(id);

            int ratesCount = book.RatesCount;

            if(isNewUser)
            {
                book.RatesCount++;
                book.Rating = ((book.Rating * (ratesCount)) + int.Parse(rate)) / (ratesCount + 1);
            }
            else
            {
                book.Rating = ((book.Rating * (ratesCount - 1)) + int.Parse(rate)) / ratesCount;
            }
           
            await this.bookRepository.UpdateAsync(book);
            return RedirectToAction("BookDetails","Book", new { id });
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
            var books = await this.bookRepository.GetPaged(page, recordsOnPageIndex);
            var booksViewModels = this.objectMapper.Map<IEnumerable<Book>, IEnumerable<BooksIndexViewModel>>(books.Results);
            return booksViewModels;
        }

        private async Task<IEnumerable<CategoryBookViewModel>> GetBooksByCategoryPage(string genreId, int page)
        {
            var books = await this.genreRepository.GetBooksByGenrePageAsync(genreId, page, recordsOnPageCategory);
            var booksViewModel = this.objectMapper.Map<IEnumerable<Book>, IEnumerable<CategoryBookViewModel>>(books.Results);
            return booksViewModel;
        }
        #endregion
    }
}