using BookSpace.Models;
using System;

namespace BookSpace.Factories
{
    public interface IBookFactory
    {
        Book Create(string bookId, string isbn, string title, DateTime publicationYear, string coverUrl);
    }
}
