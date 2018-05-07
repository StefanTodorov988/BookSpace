using BookSpace.Models;
using System;

namespace BookSpace.Factories
{
    public class CommentFactory : IFactory<Comment, CommentResponseModel>
    {
        public Comment Create(CommentResponseModel model)
        {
            var comment =  new Comment()
            {
                CommentId = Guid.NewGuid().ToString(),
                UserId = model.UserId,
                BookId = model.BookId,
                Content = model.Content,
                Date = model.Date
            };

            return comment;
        }
    }
}
