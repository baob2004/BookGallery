using BookGallery.Application.Common;
using BookGallery.Application.DTOs;
using BookGallery.Application.DTOs.BookImage;
using BookGallery.Domain.Entities;

namespace BookGallery.Application.Interfaces
{
    public interface IBookService
    {
        Task<PagedResult<BookDto>> GetPagedAsync(PageRequest request, CancellationToken ct = default);
        Task<List<BookDto>> GetAllAsync(CancellationToken ct = default);
        Task<BookDto> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<BookDto> CreateAsync(BookCreateDto dto, CancellationToken ct = default);
        Task UpdateAsync(Guid id, BookUpdateDto dto, CancellationToken ct = default);

        // Author Book
        Task AssignAuthorsAsync(Guid bookId, List<Guid> authorIds, CancellationToken ct = default);
        // Images
        Task<BookImageDto> AssignImageAsync(Guid bookId, BookImageCreateDto dto, CancellationToken ct = default);
    }
}