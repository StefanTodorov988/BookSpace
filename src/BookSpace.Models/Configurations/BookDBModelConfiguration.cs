using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSpace.Models.Configurations
{
    public class BookDBModelConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(pk => pk.BookId);

            builder.Property(p => p.Title)
                .IsRequired(true)
                .IsUnicode(true);

            builder.HasMany(c => c.Comments)
                .WithOne(b => b.Book)
                .HasForeignKey(fk => fk.BookId);

            builder.HasMany(gb => gb.BookGenres)
                .WithOne(b => b.Book)
                .HasForeignKey(fk => fk.BookId);

            builder.HasMany(bu => bu.BookUsers)
                .WithOne(b => b.Book)
                .HasForeignKey(fk => fk.BookId);

            builder.HasMany(ba => ba.BookAuthors)
                .WithOne(b => b.Book)
                .HasForeignKey(fk => fk.BookId);

            builder.HasMany(bt => bt.BookTags)
                .WithOne(b => b.Book)
                .HasForeignKey(fk => fk.BookId);
        }
    }
}
