using BookSpace.Data.Contracts;
using BookSpace.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSpace.Repositories.Contracts
{
    public  interface ITagRepository : IRepository<Tag>
    {
        Task<Tag> GetTagByNameAsync(string name);
        Task<IEnumerable<Book>> GetBooksByTagAsync(string tagId);
    }
}