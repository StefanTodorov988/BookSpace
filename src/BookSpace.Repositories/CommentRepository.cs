using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Data;
using BookSpace.Data.Contracts;
using BookSpace.Models;
using BookSpace.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookSpace.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IDbContext dbContext) : base(dbContext) { }

    }
}
