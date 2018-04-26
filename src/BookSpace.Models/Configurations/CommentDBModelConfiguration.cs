using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSpace.Models.Configurations
{
    public class CommentDBModelConfiguration : IEntityTypeConfiguration<CommentDBModel>
    {
        public void Configure(EntityTypeBuilder<CommentDBModel> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(pk => pk.CommentId);

            builder.Property(p => p.Value)
                .IsRequired(true)
                .IsUnicode(true);

            builder.HasOne(b => b.Book)
                .WithMany(c => c.Comments)
                .HasForeignKey(fk => fk.BookId);
        }
    }
}
