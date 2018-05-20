using BookSpace.Data;
using BookSpace.Data.Contracts;
using BookSpace.Factories;
using BookSpace.Models;
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
        private readonly IRepository<Genre> genreRepository;
        private readonly IRepository<Tag> tagRepository;
        private readonly IRepository<BookGenre> bookGenreRepository;
        private readonly IRepository<BookTag> bookTagRepository;

        public BookDataServices(BookSpaceContext dbCtx,
                                IRepository<Genre> genreRepository,
                                IRepository<Tag> tagRepository,
                                IRepository<BookGenre> bookGenreRepository,
                                IRepository<BookTag> bookTagRepository, 
                                UserManager<ApplicationUser> user)
        {
            this.dbCtx = dbCtx ?? throw new ArgumentNullException(nameof(dbCtx));
            this.genreRepository = genreRepository;
            this.tagRepository = tagRepository;
            this.bookGenreRepository = bookGenreRepository;
            this.bookTagRepository = bookTagRepository;
        }

        //splitting tags genres response to seperate entities
        public IEnumerable<string> FormatStringResponse(string response)
        {
            try
            {
                var handledString = Regex.Split(response, regexPatern, RegexOptions.None);
                return handledString;
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public async Task MatchGenresToBookAsync(IEnumerable<string> genres, string bookId)
        {
            foreach (var genreName in genres)
            {
                var genre = await this.genreRepository.GetAsync(g => g.Name == genreName);

                if (genre == null)
                {
                    var genreNew = new Genre()
                    {
                        GenreId = Guid.NewGuid().ToString(),
                        Name = genreName
                    };
                    genre = genreNew;

                    await this.genreRepository.AddAsync(genre);
                }
                var genreId = genre.GenreId;

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
                var tag = await this.tagRepository.GetAsync(t => t.Value == tagName);

                if (tag == null)
                {
                    var tagNew = new Tag()
                    {
                        TagId = Guid.NewGuid().ToString(),
                        Value = tagName
                    };

                    tag = tagNew;
                    await this.tagRepository.AddAsync(tag);
                }
                var tagId = tag.TagId;

                var bookTagRecord = new BookTag()
                {
                    BookId = bookId,
                    TagId = tagId
                };
                await this.bookTagRepository.AddAsync(bookTagRecord);
            }
        }

        public void MatchCommentToUser(string commentId, string userId)
        {

            var user = dbCtx.Users.Where(u => u.Id == userId).SingleOrDefault();

            var comment = dbCtx.Comments.Where(cm => cm.CommentId == commentId).SingleOrDefault();

            user.Comments.Add(comment);

            comment.User = user;

        }
    }
}
