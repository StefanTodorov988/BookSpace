using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSpace.Models.Configurations
{
    public class TagDBModelConfiguration : IEntityTypeConfiguration<TagDBModel>
    {
        public void Configure(EntityTypeBuilder<TagDBModel> builder)
        {
            builder.ToTable("Tags");

            builder.HasKey(pk => pk.TagId);
        }
    }
}
