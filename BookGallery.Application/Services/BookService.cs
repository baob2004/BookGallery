using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookGallery.Application.Common;
using BookGallery.Application.DTOs;
using BookGallery.Application.DTOs.BookImage;
using BookGallery.Application.Interfaces;
using BookGallery.Domain.Entities;

namespace BookGallery.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public BookService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AssignAuthorsAsync(Guid bookId, List<Guid> authorIds, CancellationToken ct = default)
        {
            var book = await _uow.Books.GetByIdAsync(bookId, ct: ct)
                       ?? throw new KeyNotFoundException("Book not found");

            var targetIds = authorIds.Where(x => x != Guid.Empty).Distinct().ToList();

            var existingIds = await _uow.Authors.GetExistingIdsAsync(targetIds, ct);
            var missing = targetIds.Except(existingIds).ToList();

            if (missing.Count > 0)
                throw new KeyNotFoundException($"Author(s) not found: {string.Join(", ", missing)}");

            foreach (var id in existingIds)
                await _uow.AuthorBooks.AddAsync(new AuthorBook { BookId = book.Id, AuthorId = id }, ct);

            await _uow.SaveChangesAsync(ct);
        }

        public async Task<BookImageDto> AssignImageAsync(Guid bookId, BookImageCreateDto dto, CancellationToken ct = default)
        {
            if (dto is null || string.IsNullOrWhiteSpace(dto.Url))
                throw new ArgumentException("Image url is required.");

            var book = await _uow.Books.GetByIdAsync(bookId, ct: ct)
                       ?? throw new KeyNotFoundException("Book not found");

            var entity = new BookImage
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                Url = dto.Url.Trim(),
                Caption = dto.Caption?.Trim(),
                IsCover = dto.IsCover,
                CreatedAt = DateTime.UtcNow
            };

            await _uow.BookImages.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);

            return _mapper.Map<BookImageDto>(entity);
        }


        public async Task<BookDto> CreateAsync(BookCreateDto dto, CancellationToken ct = default)
        {
            var book = _mapper.Map<Book>(dto);

            await _uow.Books.AddAsync(book, ct);
            await _uow.SaveChangesAsync(ct);
            var res = _mapper.Map<BookDto>(book);

            return res;
        }

        public async Task<List<BookDto>> GetAllAsync(CancellationToken ct = default)
        {
            var books = await _uow.Books.GetAllAsync(ct: ct);
            var res = _mapper.Map<List<BookDto>>(books);

            return res;
        }

        public async Task<BookDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var book = await _uow.Books.GetByIdAsync(id, ct: ct);
            return book == null ? throw new KeyNotFoundException("Book not found") : _mapper.Map<BookDto>(book);
        }

        public async Task<PagedResult<BookDto>> GetPagedAsync(PageRequest request, CancellationToken ct = default)
        {
            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var q = _uow.Books.Query();

            switch (request.SortBy?.Trim().ToLowerInvariant())
            {
                case "name":
                    q = request.Desc ? q.OrderByDescending(b => b.Name)
                                     : q.OrderBy(b => b.Name);
                    break;
                default:
                    q = q.OrderBy(b => b.Id);
                    break;
            }

            var totalCount = await _uow.Books.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var dataQuery = q
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var items = _mapper.Map<List<BookDto>>(dataQuery);

            return new PagedResult<BookDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Items = items
            };
        }


        public async Task UpdateAsync(Guid id, BookUpdateDto dto, CancellationToken ct = default)
        {
            var book = await _uow.Books.GetByIdAsync(id, ct: ct);
            if (book == null) throw new KeyNotFoundException("Book not found");

            _mapper.Map(dto, book);
            book.UpdatedAt = DateTime.UtcNow;

            _uow.Books.Update(book);
            await _uow.SaveChangesAsync(ct);
        }
    }
}