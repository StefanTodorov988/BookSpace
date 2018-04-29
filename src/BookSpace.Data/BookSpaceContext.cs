using System.IO;
using System.Threading.Tasks;
using BookSpace.Data.Contracts;
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

        public DbSet<Author> Authors { get; set; }

        public DbSet<BookAuthor> BooksAuthors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookGenre> BooksGenres { get; set; }

        public DbSet<BookUser> BooksUsers { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<UserAccessControl> UserAccessControl { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<BookTag> BooksTags { get; set; }

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new AuthorConfiguration());
            builder.ApplyConfiguration(new BookAuthorConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());
            builder.ApplyConfiguration(new BookGenreConfiguration());
            builder.ApplyConfiguration(new BookUserConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new GenreConfiguration());
            builder.ApplyConfiguration(new UserAccessControlConfiguration());
            builder.ApplyConfiguration(new TagConfiguration());
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
