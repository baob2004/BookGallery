using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookGallery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookGallery.Infrastructure.Data.Configurations
{
    public class AuthorBookConfiguration : IEntityTypeConfiguration<AuthorBook>
    {
        public void Configure(EntityTypeBuilder<AuthorBook> b)
        {
            b.ToTable("AuthorBooks");

            // KHÓA KÉP
            b.HasKey(x => new { x.BookId, x.AuthorId });

            b.HasIndex(x => x.AuthorId);

            b.HasOne(x => x.Book)
             .WithMany(bk => bk.AuthorBooks)
             .HasForeignKey(x => x.BookId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Author)
             .WithMany(a => a.AuthorBooks)
             .HasForeignKey(x => x.AuthorId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}