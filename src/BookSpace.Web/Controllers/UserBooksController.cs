using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookSpace.Models;
using BookSpace.Models.Enums;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Models.BookViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookSpace.Web.Controllers
{
    public class UserBooksController : Controller
    {
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly IBookUserRepository bookUserRepository;
        private readonly IMapper objectMapper;

        public UserBooksController(IApplicationUserRepository applicationUserRepository,
                                    IBookUserRepository bookUserRepository, 
                                    IMapper objectMapper)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.bookUserRepository = bookUserRepository;
            this.objectMapper = objectMapper;
        }

        public IApplicationUserRepository ApplicationUserRepository { get; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> _UserBooksPartialListAsync(string statusEnum)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }


            BookState parsedEnum = BookState.Read;
            if(!Enum.TryParse<BookState>(statusEnum, out parsedEnum))
            {
                throw new ArgumentException("Cannot parse enum");
            }

            var user = await this.applicationUserRepository.GetUserByUsernameAsync(User.Identity.Name);
            var userReadBooks = await this.applicationUserRepository.GetUserBooksAsync(user.Id, parsedEnum);
            var mappedBooksToViewModel = Mapper.Map<IEnumerable<Book>, IEnumerable<UserBookViewModel>>(userReadBooks);

            return PartialView("_AllUserBooksPartial", mappedBooksToViewModel);
        }

        public async Task<IActionResult> RemoveBook(string id)
        {
            var bookUser = await this.bookUserRepository.GetAsync(bu => bu.BookId == id);
            await this.bookUserRepository.DeleteAsync(bookUser);
            return View("Index");
        }
    }
}