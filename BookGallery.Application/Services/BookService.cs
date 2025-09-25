using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookGallery.Application.Common;
using BookGallery.Application.DTOs;
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
        public async Task<BookDto> CreateAsync(BookCreateDto dto, CancellationToken ct = default)
        {
            var book = _mapper.Map<Book>(dto);

            await _uow.Books.AddAsync(book);
            await _uow.SaveChangesAsync(ct);
            var res = _mapper.Map<BookDto>(book);

            return res;
        }

        public async Task<List<BookDto>> GetAllAsync(CancellationToken ct = default)
        {
            var books = await _uow.Books.GetAllAsync();
            var res = _mapper.Map<List<BookDto>>(books);

            return res;
        }

        public async Task<BookDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var book = await _uow.Books.GetByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDto>(book);
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


        public async Task<bool> UpdateAsync(Guid id, BookUpdateDto dto, CancellationToken ct = default)
        {
            var book = await _uow.Books.GetByIdAsync(id);
            if (book == null) return false;

            _mapper.Map(dto, book);
            book.UpdatedAt = DateTime.UtcNow;

            _uow.Books.Update(book);
            await _uow.SaveChangesAsync(ct);

            return true;
        }
    }
}