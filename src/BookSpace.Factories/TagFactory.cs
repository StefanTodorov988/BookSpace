using BookSpace.Factories.ResponseModels;
using BookSpace.Models;
using System;

namespace BookSpace.Factories
{
    public class TagFactory : IFactory<Tag, TagResponseModel>
    {
        public Tag Create(TagResponseModel model)
        {
            return new Tag()
            {
                TagId = Guid.NewGuid().ToString(),
                Value = model.Value
            };

        }
    }
}
