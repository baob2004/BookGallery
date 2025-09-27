using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookGallery.Application.DTOs.Author;

namespace BookGallery.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAllAsync(CancellationToken ct = default);
        Task<AuthorDto> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<AuthorDto> CreateAsync(AuthorCreateDto dto, CancellationToken ct = default);
        Task UpdateAsync(Guid id, AuthorUpdateDto dto, CancellationToken ct = default);
    }
}