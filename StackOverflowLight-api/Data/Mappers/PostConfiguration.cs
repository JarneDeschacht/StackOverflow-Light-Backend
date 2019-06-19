using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackOverflowLight_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.Data.Mappers
{
    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");
            builder.HasKey(p => p.PostId);
            builder.Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(100);
            builder.Property(p => p.Body)
                .IsRequired();
            builder.HasOne(p => p.Owner)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.Votes)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Answers)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
