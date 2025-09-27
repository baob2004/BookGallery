using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookGallery.Application.Interfaces;
using BookGallery.Domain.Entities;

namespace BookGallery.Domain.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task<List<Guid>> GetExistingIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
    }
}