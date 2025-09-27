using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookGallery.Application.DTOs.Author;
using BookGallery.Application.Interfaces;
using BookGallery.Domain.Entities;

namespace BookGallery.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public AuthorService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<AuthorDto> CreateAsync(AuthorCreateDto dto, CancellationToken ct = default)
        {
            var author = _mapper.Map<Author>(dto);

            await _uow.Authors.AddAsync(author, ct);
            await _uow.SaveChangesAsync(ct);
            var res = _mapper.Map<AuthorDto>(author);

            return res;
        }

        public async Task<List<AuthorDto>> GetAllAsync(CancellationToken ct = default)
        {
            var authors = await _uow.Authors.GetAllAsync(ct: ct);
            var res = _mapper.Map<List<AuthorDto>>(authors);

            return res;
        }

        public async Task<AuthorDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var author = await _uow.Authors.GetByIdAsync(id, ct: ct);
            return author == null ? throw new KeyNotFoundException("Author not found") : _mapper.Map<AuthorDto>(author);
        }

        public async Task UpdateAsync(Guid id, AuthorUpdateDto dto, CancellationToken ct = default)
        {
            var author = await _uow.Authors.GetByIdAsync(id);
            if (author == null) throw new KeyNotFoundException("Author not found");

            _mapper.Map(dto, author);

            _uow.Authors.Update(author);
            await _uow.SaveChangesAsync(ct);
        }
    }
}