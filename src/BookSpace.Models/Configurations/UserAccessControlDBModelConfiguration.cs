using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSpace.Models.Configurations
{
    public class UserAccessControlDBModelConfiguration : IEntityTypeConfiguration<UserAccessControlDBModel>
    {
        public void Configure(EntityTypeBuilder<UserAccessControlDBModel> builder)
        {
            builder.ToTable("UserAccessControl");

            builder.HasKey(pk => pk.UserId);

            builder.HasOne(u => u.User)
                .WithOne(u => u.UserAccessControl)
                .HasForeignKey<UserAccessControlDBModel>(fk => fk.UserId);

            builder.Property(p => p.RegistrationDate)
                .IsRequired(true)
                .HasDefaultValue(DateTime.Now);

            builder.Property(p => p.LastLogin)
                .IsRequired(true)
                .HasDefaultValue(DateTime.Now);

            builder.Property(p => p.LockOutEndTime)
                .IsRequired(true)
                .HasDefaultValue(DateTime.Now);

            builder.Property(p => p.BanEndTime)
                .IsRequired(true)
                .HasDefaultValue(DateTime.Now);
        }
    }
}
