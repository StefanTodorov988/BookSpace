using BookSpace.Data;
using BookSpace.Data.Contracts;
using BookSpace.Models;
using BookSpace.Repositories;
using BookSpace.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookSpace.Services
{
    public class BookDataServices
    {
        private const string regexPatern = @"[^\w-]+";


        private readonly BookSpaceContext dbCtx;
        private readonly IBookRepository bookRepository;
        private readonly IGenreRepository genreRepository;
        private readonly ITagRepository tagRepository;
        private readonly IBookGenreRepository bookGenreRepository;
        private readonly IBookTagRepository bookTagRepository;
        private readonly ICommentRepository commentRepository;

        public BookDataServices(BookSpaceContext dbCtx, IBookRepository bookRepository, IGenreRepository genreRepository, ITagRepository tagRepository,
            IBookGenreRepository bookGenreRepository, IBookTagRepository bookTagRepository, ICommentRepository commentRepository,UserManager<ApplicationUser> user)
        {
            this.dbCtx = dbCtx ?? throw new ArgumentNullException(nameof(dbCtx));
            this.bookRepository = bookRepository;
            this.genreRepository = genreRepository;
            this.tagRepository = tagRepository;
            this.bookGenreRepository = bookGenreRepository;
            this.bookTagRepository = bookTagRepository;
            this.commentRepository = commentRepository;
        }

        //splitting tags genres response to seperate entities
        public IEnumerable<string> FormatStringResponse(string response)
        {
            var handledString = Regex.Split(response, regexPatern, RegexOptions.None);
            return handledString;
        }

        public async Task MatchGenresToBookAsync(IEnumerable<string> genres, string bookId)
        {
            foreach (var genreName in genres)
            {
                var genreId = this.genreRepository.GetGenreByNameAsync(genreName).Result.GenreId;

                var bookGenreRecord = new BookGenre()
                {
                    BookId = bookId,
                    GenreId = genreId
                };

                await this.bookGenreRepository.AddAsync(bookGenreRecord);
            }
        }

        public async Task MatchTagToBookAsync(IEnumerable<string> tags, string bookId)
        {
            foreach (var tagName in tags)
            {
                var tagId = this.tagRepository.GetTagByNameAsync(tagName).Result.TagId;

                var bookTagRecord = new BookTag()
                {
                    BookId = bookId,
                    TagId = tagId
                };
                await this.bookTagRepository.AddAsync(bookTagRecord);
            }
        }

        public void  MatchCommentToUser(string commentId, string userId)
        {

            var user = dbCtx.Users.Where(u => u.Id == userId).SingleOrDefault();

            var comment = dbCtx.Comments.Where(cm => cm.CommentId == commentId).SingleOrDefault();

            user.Comments.Add(comment);

            comment.User = user;

        }
    }
}
