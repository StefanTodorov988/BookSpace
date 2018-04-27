using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Http;

namespace BookSpace.Models
{
    public class AuthorDBModel
    {
        public string AuthorId { get; set; }

        public string Name { get; set; }

        public ICollection<BookAuthor> AuthorBooks { get; set; }

    }
}
