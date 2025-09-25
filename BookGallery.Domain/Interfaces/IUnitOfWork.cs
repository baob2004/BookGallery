using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookGallery.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IBookRepository Books { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}