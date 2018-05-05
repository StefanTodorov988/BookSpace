using BookSpace.Data;
using BookSpace.Data.Contracts;
using BookSpace.Models;
using BookSpace.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSpace.Repositories
{
    public class BookGenreRepository : BaseRepository<BookGenre> , IBookGenreRepository
    {
        public BookGenreRepository(IDbContext dbCtx) : base(dbCtx) { }
    }
}
