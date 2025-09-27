using BookGallery.Application.Interfaces;
using BookGallery.Domain.Entities;
using BookGallery.Domain.Interfaces;
using BookGallery.Infrastructure.Data;

namespace BookGallery.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly AppDbContext _context;
        private readonly IBookRepository _bookRepo;
        private readonly IAuthorRepository _authorRepo;
        private readonly IGenericRepository<AuthorBook> _authorBookRepo;
        private readonly IGenericRepository<BookImage> _bookImageRepo;
        public UnitOfWork
        (
            AppDbContext context,
            IBookRepository bookRepo,
            IAuthorRepository authorRepo,
            IGenericRepository<AuthorBook> authorBookRepo,
            IGenericRepository<BookImage> bookImageRepo
        )
        {
            _context = context;
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
            _authorBookRepo = authorBookRepo;
            _bookImageRepo = bookImageRepo;
        }

        public IBookRepository Books => _bookRepo;

        public IAuthorRepository Authors => _authorRepo;

        public IGenericRepository<AuthorBook> AuthorBooks => _authorBookRepo;

        public IGenericRepository<BookImage> BookImages => _bookImageRepo;

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