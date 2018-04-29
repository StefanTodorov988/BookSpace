<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
=======
﻿using BookSpace.Models.Enums;
using Microsoft.EntityFrameworkCore;
>>>>>>> 280e0ded4b43c1723fcd4027699ec9ba290e71ec
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSpace.Models.Configurations
{
    public class BookUserConfiguration : IEntityTypeConfiguration<BookUser>
    {
        public void Configure(EntityTypeBuilder<BookUser> builder)
        {
            builder.ToTable("BooksUsers");

            builder.HasKey(pk => new { pk.BookId, pk.UserId });

<<<<<<< HEAD
            builder.Property(p => p.IsRead)
                .HasDefaultValue(false);
=======
>>>>>>> 280e0ded4b43c1723fcd4027699ec9ba290e71ec
        }
    }
}
