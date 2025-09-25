using BookGallery.Application.Interfaces;
using BookGallery.Infrastructure.Data;

namespace BookGallery.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly AppDbContext _context;
        private readonly IBookRepository _bookRepo;
        public UnitOfWork
        (
            AppDbContext context,
            IBookRepository bookRepo
        )
        {
            _context = context;
            _bookRepo = bookRepo;
        }

        public IBookRepository Books => _bookRepo;

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await _context.SaveChangesAsync(ct);
        }
    }
}