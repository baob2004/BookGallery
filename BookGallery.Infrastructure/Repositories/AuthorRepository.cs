using BookGallery.Domain.Entities;
using BookGallery.Domain.Interfaces;
using BookGallery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookGallery.Infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {

        public AuthorRepository(AppDbContext context) : base(context)
        {
        }
        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
            => _context.Authors.AsNoTracking().AnyAsync(a => a.Id == id, ct);

        public Task<List<Guid>> GetExistingIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
            => _context.Authors.AsNoTracking()
                 .Where(a => ids.Contains(a.Id))
                 .Select(a => a.Id)
                 .ToListAsync(ct);

    }
}