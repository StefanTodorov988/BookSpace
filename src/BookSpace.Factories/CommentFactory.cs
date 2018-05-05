using BookSpace.Models;
using System;

namespace BookSpace.Factories
{
    public class CommentFactory : IFactory<Comment, CommentResponseObject>
    {
        //TODO:NOTFINISHED
        public Comment Create(CommentResponseObject model)
        {
            return new Comment()
            {
                CommentId = Guid.NewGuid().ToString(),
                Content = model.Content,
                Date = model.Date
            };
        }
    }
}
