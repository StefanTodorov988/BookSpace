using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Models.CommentViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookSpace.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly ICommentRepository commentRepository;
        private readonly IBookRepository bookRepository;
        private readonly IMapper objectMapper;

        public CommentController(ICommentRepository commentRepository, IBookRepository bookRepository, IMapper objectMapper, IApplicationUserRepository applicationUserRepository)
        {
            this.commentRepository = commentRepository;
            this.bookRepository = bookRepository;
            this.objectMapper = objectMapper;
            this.applicationUserRepository = applicationUserRepository;
        }

        public IActionResult GetEditModal(string commentId)
        {
            return PartialView("_CommentEditPartial");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCommentAsync(string commentId, string bookId, string userId)
        {
            if(commentId == null || bookId == null || userId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var isAdmin = this.User.IsInRole("Admin");

            var currentLoggedUser = await this.applicationUserRepository.GetAsync(u => u.Id == userId);

            var isOwner = currentLoggedUser.Id == userId;

            if(!isAdmin && !isOwner)
            {
                return RedirectToAction("Index", "Home");
            }

            var commentToDelete = await this.commentRepository.GetAsync(c => c.CommentId == commentId);
            await this.commentRepository.DeleteAsync(commentToDelete);

            var comments = await this.bookRepository.GetBookCommentsAsync(bookId);
            var commentsViewModel = this.objectMapper.Map<IEnumerable<CommentViewModel>>(comments);

            foreach (var c in commentsViewModel)
            {
                c.CanEdit = isOwner || isAdmin;
            }

            return PartialView("Book/_BookCommentsPartial", new KeyValuePair<string, IEnumerable<CommentViewModel>>(bookId, commentsViewModel));
        }
    }
}
