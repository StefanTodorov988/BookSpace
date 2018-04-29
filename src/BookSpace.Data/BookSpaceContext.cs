<<<<<<< HEAD
<<<<<<< HEAD
﻿using BookSpace.Data.Contracts;
=======
﻿using System.Threading.Tasks;
=======
﻿using System.IO;
using System.Threading.Tasks;
>>>>>>> ee9ab0c912d3d49fe53b47164550140fd4f6681d
using BookSpace.Data.Contracts;
>>>>>>> 280e0ded4b43c1723fcd4027699ec9ba290e71ec
using BookSpace.Models;
using BookSpace.Models.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookSpace.Data
{
    public class BookSpaceContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public BookSpaceContext(DbContextOptions<BookSpaceContext> options) 
            : base(options)
        {
        }

<<<<<<< HEAD
<<<<<<< HEAD
        public DbSet<AuthorDBModel> Authors { get; set; }

        public DbSet<BookAuthor> BooksAuthors { get; set; }

        public DbSet<BookDBModel> Books { get; set; }

        public DbSet<BookGenre> BooksGenres { get; set; }

        public DbSet<BookUser> BooksUsers { get; set; }

        public DbSet<CommentDBModel> Comments { get; set; }

        public DbSet<GenreDBModel> Genres { get; set; }
=======
        public virtual DbSet<Author> Authors { get; set; }
=======
        public DbSet<Author> Authors { get; set; }
>>>>>>> ee9ab0c912d3d49fe53b47164550140fd4f6681d

        public DbSet<BookAuthor> BooksAuthors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookGenre> BooksGenres { get; set; }

        public DbSet<BookUser> BooksUsers { get; set; }

        public DbSet<Comment> Comments { get; set; }

<<<<<<< HEAD
        public virtual DbSet<Genre> Genres { get; set; }
>>>>>>> 280e0ded4b43c1723fcd4027699ec9ba290e71ec
=======
        public DbSet<Genre> Genres { get; set; }
>>>>>>> ee9ab0c912d3d49fe53b47164550140fd4f6681d

        public DbSet<UserAccessControl> UserAccessControl { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<BookTag> BooksTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
<<<<<<< HEAD
            builder.ApplyConfiguration(new AuthorDBModelConfiguration());
            builder.ApplyConfiguration(new BookAuthorConfiguration());
            builder.ApplyConfiguration(new BookDBModelConfiguration());
            builder.ApplyConfiguration(new BookGenreConfiguration());
            builder.ApplyConfiguration(new BookUserConfiguration());
            builder.ApplyConfiguration(new CommentDBModelConfiguration());
            builder.ApplyConfiguration(new GenreDBModelConfiguration());
            builder.ApplyConfiguration(new UserAccessControlDBModelConfiguration());
            builder.ApplyConfiguration(new TagDBModelConfiguration());
=======
            builder.ApplyConfiguration(new AuthorConfiguration());
            builder.ApplyConfiguration(new BookAuthorConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());
            builder.ApplyConfiguration(new BookGenreConfiguration());
            builder.ApplyConfiguration(new BookUserConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new GenreConfiguration());
            builder.ApplyConfiguration(new UserAccessControlConfiguration());
            builder.ApplyConfiguration(new TagConfiguration());
>>>>>>> 280e0ded4b43c1723fcd4027699ec9ba290e71ec
            builder.ApplyConfiguration(new BookTagConfiguration());
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {

            return this.Set<TEntity>();
        }

        public async Task<int> SaveAsync()
        {
            return await this.SaveChangesAsync();
        }
    }
}
