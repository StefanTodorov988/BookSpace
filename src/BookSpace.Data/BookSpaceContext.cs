using System;
using System.Collections.Generic;
using System.Text;
using BookSpace.Data.Contracts;
using BookSpace.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookSpace.Data
{
    public class BookSpaceContext : IdentityDbContext<User>, IDbContext 
    {
        public BookSpaceContext(DbContextOptions options) : base(options)
        {
        }

       // public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Add configurations

            //builder.ApplyConfiguration();
        }

    }
}
