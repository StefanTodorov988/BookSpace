using System.Threading.Tasks;
using BookSpace.Data.Contracts;
using BookSpace.Models;
using BookSpace.Models.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookSpace.Data
{
    public class BookSpaceContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public BookSpaceContext(DbContextOptions<BookSpaceContext> options) 
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual  DbSet<BookAuthor> BooksAuthors { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<BookGenre> BooksGenres { get; set; }

        public virtual DbSet<BookUser> BooksUsers { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        //public DbSet<UserAccessControlDBModel> UserAccessControl { get; set; }

        //public DbSet<TagDBModel> Tags { get; set; }

        //public DbSet<BookTag> BooksTags { get; set; }

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

        public virtual DbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public virtual void SetAdded<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Added;
        }

        public virtual void SetDeleted<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public virtual void SetUpdated<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public Task<int> SaveAsync()
        {
            return this.SaveChangesAsync();
        }
    }
}
