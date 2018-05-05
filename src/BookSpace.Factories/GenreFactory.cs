using BookSpace.Models;
using System;

namespace BookSpace.Factories
{
    public class GenreFactory : IFactory<Genre, GenreResponseModel>
    {
        public Genre Create(GenreResponseModel model)
        {
            return new Genre()
            {
                GenreId = Guid.NewGuid().ToString(),
                Name = model.Name
            };
        }
    }
}
