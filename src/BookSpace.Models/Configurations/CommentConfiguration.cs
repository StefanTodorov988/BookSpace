using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSpace.Models.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(pk => pk.CommentId);

            builder.Property(p => p.Content)
                .IsRequired(true)
                .IsUnicode(true);

            builder.HasOne(b => b.Book)
                .WithMany(c => c.Comments)
                .HasForeignKey(fk => fk.BookId);
        }
    }
}
