using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSpace.Models.Configurations
{
    public class GenreDBModelConfiguration : IEntityTypeConfiguration<GenreDBModel>
    {
        public void Configure(EntityTypeBuilder<GenreDBModel> builder)
        {
            builder.ToTable("Genres");

            builder.HasKey(pk => pk.GenreId);

            builder.Property(p => p.Name)
                .IsRequired(true)
                .IsUnicode(true);

            builder.HasMany(gb => gb.GenreBooks)
                .WithOne(g => g.Genre)
                .HasForeignKey(fk => fk.GenreId);
        }
    }
}
