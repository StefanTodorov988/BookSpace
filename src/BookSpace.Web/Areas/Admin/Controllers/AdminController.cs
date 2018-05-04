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
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Areas.Admin.Models.ApplicationUserViewModels;
using BookSpace.Web.Models.BookViewModels;
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
        private readonly IMapper objectMapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBlobStorageService blobStorageService;
        private readonly IFactory<Book, BookResponseModel> bookFactory;

        public AdminController(IApplicationUserRepository userRepository, IBookRepository bookRepository,
            IMapper objectMapper, UserManager<ApplicationUser> userManager, IBlobStorageService blobStorageService,
            IFactory<Book,BookResponseModel> bookFactory)
        {
            this.userRepository = userRepository;
            this.bookRepository = bookRepository;
            this.objectMapper = objectMapper;
            this.userManager = userManager;
            this.blobStorageService = blobStorageService;
            this.bookFactory = bookFactory;
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

        public IActionResult AllBooks()
        {
            var allBooks = this.bookRepository.GetAllAsync().Result.ToList();

            var mappedBooks = this.objectMapper.Map<IEnumerable<ListBookViewModel>>(allBooks);

            return View(mappedBooks);
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

        [HttpPost("/CreateBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBookAsync(CreateBookViewModel bookViewModel)
        {
            var bookResponse = this.objectMapper.Map<BookResponseModel>(bookViewModel);

            var book = this.bookFactory.Create(bookResponse);

            
            await this.bookRepository.AddAsync(book);

            return RedirectToAction("CreateBook");
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