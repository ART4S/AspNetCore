﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.DataContext.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.OwnsOne(x => x.ContactDetails, b =>
            {
                b.Property(x => x.Email).IsRequired();
                b.Property(x => x.PhoneNumber).IsRequired(false);
            });
        }
    }
}
