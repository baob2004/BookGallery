using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookGallery.Domain.Entities;
using BookGallery.Domain.Interfaces;

namespace BookGallery.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        IGenericRepository<AuthorBook> AuthorBooks { get; }
        IGenericRepository<BookImage> BookImages { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}