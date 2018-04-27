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

        public DbSet<AuthorDBModel> Authors { get; set; }

        public DbSet<BookAuthor> BooksAuthors { get; set; }

        public DbSet<BookDBModel> Books { get; set; }

        public DbSet<BookGenre> BooksGenres { get; set; }

        public DbSet<BookUser> BooksUsers { get; set; }

        public DbSet<CommentDBModel> Comments { get; set; }

        public DbSet<GenreDBModel> Genres { get; set; }

        //public DbSet<UserAccessControlDBModel> UserAccessControl { get; set; }

        //public DbSet<TagDBModel> Tags { get; set; }

        //public DbSet<BookTag> BooksTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new AuthorDBModelConfiguration());
            builder.ApplyConfiguration(new BookAuthorConfiguration());
            builder.ApplyConfiguration(new BookDBModelConfiguration());
            builder.ApplyConfiguration(new BookGenreConfiguration());
            builder.ApplyConfiguration(new BookUserConfiguration());
            builder.ApplyConfiguration(new CommentDBModelConfiguration());
            builder.ApplyConfiguration(new GenreDBModelConfiguration());
            builder.ApplyConfiguration(new UserAccessControlDBModelConfiguration());
            builder.ApplyConfiguration(new TagDBModelConfiguration());
            builder.ApplyConfiguration(new BookTagConfiguration());
        }

        public DbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public void SetAdded<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Added;
        }

        public void SetDeleted<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public void SetUpdated<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }
}
