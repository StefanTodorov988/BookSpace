using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSpace.Models.Configurations
{
    public class AuthorDBModelConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");

            builder.HasKey(pk => pk.AuthorId);

            builder.Property(p => p.Name)
                .IsRequired(true)
                .IsUnicode(true);

            builder.HasMany(ab => ab.AuthorBooks)
                .WithOne(a => a.Author)
                .HasForeignKey(fk => fk.AuthorId);
        }
    }
}
