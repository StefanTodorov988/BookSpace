using BookSpace.Data;
using BookSpace.Data.Contracts;
using BookSpace.Models;
using BookSpace.Repositories;
using BookSpace.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookSpace.Services
{
    public class BookDataServices
    {
        private const string regexPatern = @"[^\w-]+";


        private readonly IDbContext dbContext;
        private readonly IBookRepository bookRepository;
        private readonly IGenreRepository genreRepository;
        private readonly ITagRepository tagRepository;
        private readonly IBookGenreRepository bookGenreRepository;
        private readonly IBookTagRepository bookTagRepository;
        private readonly ICommentRepository commentRepository;

        public BookDataServices(IDbContext dbCtx,IBookRepository bookRepository, IGenreRepository genreRepository, ITagRepository tagRepository,
            IBookGenreRepository bookGenreRepository, IBookTagRepository bookTagRepository,ICommentRepository commentRepository)
        {
            this.dbContext = dbCtx ?? throw new ArgumentNullException(nameof(dbContext));
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
                //await this.dbContext.DbSet<BookGenre>().AddAsync(bookGenreRecord);
            }
            //await this.dbContext.SaveAsync();
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
                //await this.dbContext.DbSet<BookTag>().AddAsync(bookTagRecord);
            }
            //await this.dbContext.SaveAsync();
        }

        public async Task MatchCommentToBook(string commentId, string  bookId)
        {
            var comment = this.commentRepository.GetByIdAsync(commentId).Result;

            var book = this.bookRepository.GetByIdAsync(bookId).Result;

            book.Comments.Add(comment);

            
        }
    }
}
