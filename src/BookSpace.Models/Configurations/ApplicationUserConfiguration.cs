using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSpace.Models.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(btr => btr.BookUsers)
                .WithOne(bu => bu.User)
                .HasForeignKey(fk => fk.UserId);

            builder.HasMany(c => c.Comments)
                .WithOne(u => u.User)
                .HasForeignKey(fk => fk.UserId);
        }
    }
}
