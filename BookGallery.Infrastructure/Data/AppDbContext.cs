using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookGallery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookGallery.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<AuthorBook> AuthorBooks => Set<AuthorBook>();
        public DbSet<BookImage> BookImages => Set<BookImage>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}