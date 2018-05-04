﻿using BookSpace.Data.Contracts;
using BookSpace.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSpace.Repositories
{
    public  interface IGenreRepository : IRepository<Genre>
    {
        Task<Genre> GetGenreByNameAsync(string name);
        Task<IEnumerable<Book>> GetBooksWithGenreAsync(string genreId);
    }
}