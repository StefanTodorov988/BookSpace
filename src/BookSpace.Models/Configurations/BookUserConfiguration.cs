using BookSpace.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSpace.Models.Configurations
{
    public class BookUserConfiguration : IEntityTypeConfiguration<BookUser>
    {
        public void Configure(EntityTypeBuilder<BookUser> builder)
        {
            builder.Property(p => p.HasRatedBook)
               .HasDefaultValue(false);

            builder.ToTable("BooksUsers");

            builder.HasKey(pk => new { pk.BookId, pk.UserId });

        }
    }
}
