﻿using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataContext.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Content).IsRequired();

            builder.HasOne(x => x.Blog)
                .WithMany()
                .HasForeignKey(x => x.BlogId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
