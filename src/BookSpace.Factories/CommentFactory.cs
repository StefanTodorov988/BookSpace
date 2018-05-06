using BookSpace.Models;
using System;

namespace BookSpace.Factories
{
    public class CommentFactory : IFactory<Comment, CommentResponseModel>
    {
        //TODO:NOTFINISHED
        public Comment Create(CommentResponseModel model)
        {
            return new Comment()
            {
                CommentId = Guid.NewGuid().ToString(),
                UserId  = model.UserId,
                BookId= model.BookId,
                Content = model.Content,
                Date = model.Date
            };
        }
    }
}
