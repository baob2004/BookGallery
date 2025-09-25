using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookGallery.Application.Interfaces;
using BookGallery.Domain.Entities;
using BookGallery.Infrastructure.Data;

namespace BookGallery.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }
    }
}