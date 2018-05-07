using AutoMapper;
using BookSpace.Factories;
using BookSpace.Models;
using BookSpace.Repositories;
using BookSpace.Repositories.Contracts;
using BookSpace.Services;
using BookSpace.Web.Models.BookViewModels;
using BookSpace.Web.Models.CommentsViewModel;
using BookSpace.Web.Models.GenreViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
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
        private readonly ITagRepository tagRepository;
        private readonly IBookUserRepository bookUserRepository;
        private readonly ICommentRepository commentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFactory<Comment, CommentResponseModel> commentFactory;
        private readonly BookDataServices dataService;
        private const int recordsOnPageIndex = 30;
        private const int recordsOnPageCategory = 10;

        public BookController(IBookRepository bookRepository,
                              IGenreRepository genreRepository,
                              ITagRepository tagRepository,
                              IBookUserRepository bookUserRepository,
                              ICommentRepository commentRepository,
                              UserManager<ApplicationUser> userManager,
                              IFactory<Comment, CommentResponseModel> commentFactory,
                              BookDataServices dataService,
                              IMapper objectMapper)
        {
            this.bookRepository = bookRepository;
            this.genreRepository = genreRepository;
            this.tagRepository = tagRepository;
            this.bookUserRepository = bookUserRepository;
            this.commentRepository = commentRepository;
            this._userManager = userManager;
            this.commentFactory = commentFactory;
            this.dataService = dataService;
            this.objectMapper = objectMapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var indexViewModel = new AllBooksViewModel()
            {
                Books = await this.GetBooksPage(page),
                BooksCount = await this.bookRepository.GetCount()
            };

            return View(indexViewModel);
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
        public async Task<IActionResult> BooksByCategoryList([FromRoute] string id, [FromQuery] int page)
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

            foreach (var comment in comments)
            {
                var user = await this._userManager.FindByIdAsync(comment.UserId);

                comment.User = user;
            }

            var genres = await this.bookRepository.GetBookGenresAsync(id);
            var tags = await this.bookRepository.GetBookTagsAsync(id);
            var bookUser = await this.bookUserRepository.GetAsync(bu => bu.BookId == id);

            var bookViewModel = this.objectMapper.Map<Book, BookViewModel>(book);
            var commentsViewModel = this.objectMapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);
            var propertiesViewModel = new BookPropertiesViewModel();
            propertiesViewModel.Comments = commentsViewModel;
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

            if (isNewUser)
            {
                book.RatesCount++;
                book.Rating = ((book.Rating * (ratesCount)) + int.Parse(rate)) / (ratesCount + 1);
            }
            else
            {
                book.Rating = ((book.Rating * (ratesCount - 1)) + int.Parse(rate)) / ratesCount;
            }

            await this.bookRepository.UpdateAsync(book);
            return RedirectToAction("BookDetails", "Book", new { id });
        }

        public IActionResult GetBookGenres(string bookId)
        {
            //TODO:Not finished
            var dbModel = this.bookRepository.GetBookGenresAsync(bookId);
            var mappedGenreViewModel = this.objectMapper.Map<GenreViewModel>(dbModel);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string id, string comment, string userId)
        {
            //creating response object from input
            var commentResponse = this.commentFactory.Create(new CommentResponseModel()
            {
                //TODO: NOT WORKING WITH USERID
                UserId = userId,
                BookId = id,
                Content = comment,
                Date = DateTime.Now
            });

            await this.commentRepository.AddAsync(commentResponse);

            return RedirectToAction("BookDetails", new { id });
        }

        public async Task<IActionResult> Search(string filter, string filterRadio = "default")
        {
            List<Book> foundBooks = new List<Book>();
            if (filterRadio == "default")
            {
                foundBooks = new List<Book>(await bookRepository.Search(x => x.Title.Contains(filter) || x.Author.Contains(filter)));
            }
            else if (filterRadio == "title")
            {
                foundBooks = new List<Book>(await bookRepository.Search(x => x.Title.Contains(filter)));
            }
            else if (filterRadio == "author")
            {
                foundBooks = new List<Book>(await bookRepository.Search(x => x.Author.Contains(filter)));
            }
            else if (filterRadio == "genre")
            {
                foundBooks = new List<Book>(await this.genreRepository.GetBooksByGenreNameAsync(filter));
            }
            else if(filterRadio == "tag")
            {
                foundBooks = new List<Book>(await this.tagRepository.GetBooksByTagAsync(filter));
            }

            var foundBooksViewModel = this.objectMapper.Map<IEnumerable<Book>, IEnumerable<SearchedBookViewModel>>(foundBooks);

            return View("Search" , foundBooksViewModel);
        }

        public async Task<IEnumerable<Book>> SerachByGenre(string filter)
        {

            var foundBooks = await bookRepository.SearchByNavigationProperty
                                    ("BookGenres", "Genre", b => CheckBookGenres(b, filter));
            return foundBooks;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> SerachByTag(string filter)
        {

            var foundBooks = await bookRepository.SearchByNavigationProperty
                                   ("BookTags", "Tag", b => CheckBookTags(b, filter));

            return foundBooks;
        }

        #region Helpers

        private async Task<IEnumerable<BooksIndexViewModel>> GetBooksPage(int page)
        {
            var books = await this.bookRepository.GetPaged(page, recordsOnPageIndex);
            var booksViewModels = this.objectMapper.Map<IEnumerable<Book>, IEnumerable<BooksIndexViewModel>>(books.Results);
            return booksViewModels;
        }

        private async Task<IEnumerable<BookByCategoryViewModel>> GetBooksByCategoryPage(string genreId, int page)
        {
            var books = await this.genreRepository.GetBooksByGenrePageAsync(genreId, page, recordsOnPageCategory);
            var booksViewModel = this.objectMapper.Map<IEnumerable<Book>, IEnumerable<BookByCategoryViewModel>>(books.Results);
            return booksViewModel;
        }

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