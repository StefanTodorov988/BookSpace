using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSpace.Models.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
<<<<<<< HEAD
            builder.HasMany(btr => btr.Books)
=======
            builder.HasMany(btr => btr.BookUsers)
>>>>>>> 280e0ded4b43c1723fcd4027699ec9ba290e71ec
                .WithOne(bu => bu.User)
                .HasForeignKey(fk => fk.UserId);

            builder.HasMany(c => c.Comments)
                .WithOne(u => u.User)
                .HasForeignKey(fk => fk.UserId);
        }
    }
}
