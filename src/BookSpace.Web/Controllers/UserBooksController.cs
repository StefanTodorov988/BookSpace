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
        private readonly IMapper objectMapper;

        public UserBooksController(IApplicationUserRepository applicationUserRepository, IMapper objectMapper)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.objectMapper = objectMapper;
        }

        public IApplicationUserRepository ApplicationUserRepository { get; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult _ReadBooksPartialListAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = this.applicationUserRepository.GetUserByUsernameAsync(User.Identity.Name).Result;
            var userReadBooks = this.applicationUserRepository.GetUserBooksAsync(user.Id, BookState.Read).Result;

            IEnumerable<UserBookViewModel> mappedBooksToViewModel = null; 

            if(userReadBooks != null)
            {
                mappedBooksToViewModel = Mapper.Map<IEnumerable<Book>, IEnumerable<UserBookViewModel>>(userReadBooks);
            }

            return PartialView("_ReadBooksPartial", mappedBooksToViewModel);
        }
    }
}