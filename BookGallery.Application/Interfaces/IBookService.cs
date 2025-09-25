using BookGallery.Application.Common;
using BookGallery.Application.DTOs;

namespace BookGallery.Application.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllAsync(CancellationToken ct = default);
        Task<BookDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<BookDto> CreateAsync(BookCreateDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, BookUpdateDto dto, CancellationToken ct = default);

        Task<PagedResult<BookDto>> GetPagedAsync(PageRequest request, CancellationToken ct = default);
    }
}