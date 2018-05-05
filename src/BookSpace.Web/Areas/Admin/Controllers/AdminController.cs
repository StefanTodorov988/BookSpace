using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookSpace.BlobStorage.Contracts;
using BookSpace.Factories;
using BookSpace.Factories.ResponseModels;
using BookSpace.Models;
using BookSpace.Repositories;
using BookSpace.Repositories.Contracts;
using BookSpace.Services;
using BookSpace.Web.Areas.Admin.Models.ApplicationUserViewModels;
using BookSpace.Web.Models.BookViewModels;
using BookSpace.Web.Models.GenreViewModels;
using BookSpace.Web.Services.SmtpService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSpace.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IApplicationUserRepository userRepository;
        private readonly IBookRepository bookRepository;
        private readonly ITagRepository tagRepository;
        private readonly IGenreRepository genreRepository;
        private readonly IMapper objectMapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly BookServices bookServices;
        private readonly IBlobStorageService blobStorageService;
        private readonly IFactory<Book, BookResponseModel> bookFactory;
        private readonly IFactory<Genre, GenreResponseModel> genreFactory;
        private readonly IFactory<Tag, TagResponseModel> tagFactory;

        public AdminController(IApplicationUserRepository userRepository, IBookRepository bookRepository, ITagRepository tagRepository, IGenreRepository genreRepository,
            IFactory<Book, BookResponseModel> bookFactory, IFactory<Genre, GenreResponseModel> genreFactory, IFactory<Tag, TagResponseModel> tagFactory,
            IMapper objectMapper, UserManager<ApplicationUser> userManager, BookServices bookServices, IBlobStorageService blobStorageService)
        {
            this.userRepository = userRepository;
            this.bookRepository = bookRepository;
            this.tagRepository = tagRepository;
            this.genreRepository = genreRepository;
            this.objectMapper = objectMapper;
            this.userManager = userManager;
            this.bookServices = bookServices;
            this.blobStorageService = blobStorageService;
            this.bookFactory = bookFactory;
            this.genreFactory = genreFactory;
            this.tagFactory = tagFactory;
        }

        public IActionResult AllUsers()
        {
            var users = this.userRepository.GetAllAsync().Result.ToList();

            var userViewModels = new List<ApplicationUserViewModel>();

            foreach (var user in users)
            {
                userViewModels.Add(this.objectMapper.Map<ApplicationUserViewModel>(user));
            }

            return View(userViewModels);
        }

        public IActionResult AllBooks()
        {
            var allBooks = this.bookRepository.GetAllAsync().Result.ToList();

            var mappedBooks = this.objectMapper.Map<IEnumerable<ListBookViewModel>>(allBooks);

            return View(mappedBooks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(ApplicationUserViewModel userViewModel)
        {
            //TODO:Getting user by anything that can be changed is impossible!So I must use Id and therefore ID cannot be changed which is not good!
            var dbModel = this.userManager.FindByIdAsync(userViewModel.Id).Result;

            var user = this.objectMapper.Map(userViewModel, dbModel);

            if (userViewModel.isAdmin)
            {
                await this.userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                await this.userManager.RemoveFromRoleAsync(user, "Admin");
            }

            await this.userManager.UpdateAsync(user);

            return this.RedirectToAction("AllUsers");
        }

        public IActionResult EditUser(string id)
        {
            var dbModel = this.userRepository.GetByIdAsync(id).Result;
            var userViewModel = objectMapper.Map<ApplicationUserViewModel>(dbModel);

            return View(userViewModel);
        }



        [HttpPost("/EditBook/{bookid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(DetailedBookViewModel bookViewModel)
        {
            var dbModel = this.bookRepository.GetByIdAsync(bookViewModel.BookId).Result;

            var book = this.objectMapper.Map(bookViewModel, dbModel);

            await this.bookRepository.UpdateAsync(book);

            return this.RedirectToAction("AllBooks");
        }

        [HttpGet("/EditBook/{bookid}")]
        public IActionResult EditBook(string bookId)
        {

            var dbModel = this.bookRepository.GetByIdAsync(bookId).Result;

            var bookViewModel = objectMapper.Map<DetailedBookViewModel>(dbModel);

            return View(bookViewModel);
        }

        public async Task<IActionResult> DeleteBook(ListBookViewModel bookViewModel)
        {
            var dbModel = this.bookRepository.GetByIdAsync(bookViewModel.BookId).Result;

            await this.bookRepository.RemoveBookAync(dbModel.BookId);

            return this.RedirectToAction("AllBooks");
        }


        [HttpGet("/CreateBook")]
        public IActionResult CreateBook()
        {
            return View();
        }

        //TODO:MAJOR FEATURES MISSING
        [HttpPost("/CreateBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBookAsync(CreateBookViewModel bookViewModel)
        {
            var bookResponse = this.objectMapper.Map<BookResponseModel>(bookViewModel);
            var book = this.bookFactory.Create(bookResponse);

            await this.bookRepository.AddAsync(book);

            var genres = this.bookServices.FormatStringResponse(bookViewModel.Genres);
            var tags = this.bookServices.FormatStringResponse(bookViewModel.Tags);

            await this.bookServices.MatchGenresToBookAsync(genres, book.BookId);
            await this.bookServices.MatchTagToBookAsync(tags, book.BookId);

            //ADDING GENRES TO BOOK
            //search for genres with the names from the controller in the database  get their Ids
            //create new BookGenre record with bookId and GenreId add it to database

            //SAMPLE CODE 
            //foreach (var genre in bookViewModel.Genres)
            //{
            //    var genreToBook = await this.genreRepository.GetGenreByNameAsync(genre);

            //    //This would work if we have BookGenre repository because AddAsync method works only with its appropriate type(e.g. for GenreRepository => genres)
            //    this.genreRepository.AddAsync(new BookGenre()
            //    {
            //        BookId = bookViewModel.BookId,
            //        GenreId = genreToBook.GenreId
            //    });
            //}

            //ADDING TAGS TO BOOK
            //search for tags with the names from the controller in the database get their Ids
            //create new BookTag record with bookId and TagId and add it to database

            //SAMPLE CODE
            //Same as above just with genres



            return RedirectToAction("CreateBook");
        }

        [HttpGet("/CreateTag")]
        public IActionResult CreateTag()
        {
            return View();
        }

        [HttpPost("/CreateTag")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTagAsync(TagViewModel tagViewModel)
        {
            var tagResponse = this.objectMapper.Map<TagResponseModel>(tagViewModel);

            var tag = this.tagFactory.Create(tagResponse);

            await this.tagRepository.AddAsync(tag);

            return RedirectToAction("CreateTag");
        }

        [HttpGet("/CreateGenre")]
        public IActionResult CreateGenre()
        {
            return View();
        }

        [HttpPost("/CreateGenre")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenreAsync(GenreViewModel genreViewModel)
        {
            var genreResponse = this.objectMapper.Map<GenreResponseModel>(genreViewModel);

            var genre = this.genreFactory.Create(genreResponse);

            await this.genreRepository.AddAsync(genre);

            return RedirectToAction("CreateGenre");
        }

        public IActionResult Index()
        {
            //using (FileStream str = new FileStream(@"C:\Users\snikoltc\Documents\Visual Studio 2017\Projects\BookSpace\src\BookSpace.Web\wwwroot\images\art\basquiat-selft-portret.jpg", FileMode.Open))
            //{
            //    await blobStorageService.UploadAsync("testName", "testcontainer", str);
            //    var result = await blobStorageService.GetAsync("testName", "testcontainer");
            //}          
            return View();
        }
    }
}