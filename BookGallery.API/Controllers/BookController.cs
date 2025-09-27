using BookGallery.Application.Common;
using BookGallery.Application.DTOs;
using BookGallery.Application.DTOs.BookImage;
using BookGallery.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookGallery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookSvc;
        public BookController(IBookService bookSvc)
        {
            _bookSvc = bookSvc;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<BookDto>>> GetAll([FromQuery] PageRequest request, CancellationToken ct = default)
        {
            var res = await _bookSvc.GetPagedAsync(request, ct);
            return Ok(res);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookDto>> GetById(Guid id, CancellationToken ct = default)
        {
            var res = await _bookSvc.GetByIdAsync(id, ct);
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> Create(BookCreateDto dto, CancellationToken ct = default)
        {
            var res = await _bookSvc.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, BookUpdateDto dto, CancellationToken ct = default)
        {
            await _bookSvc.UpdateAsync(id, dto, ct);
            return NoContent();
        }

        [HttpPost("{id:guid}/authors")]
        public async Task<IActionResult> AssignAuthors(Guid id, List<Guid> authorIds, CancellationToken ct = default)
        {
            await _bookSvc.AssignAuthorsAsync(id, authorIds, ct);
            return NoContent();
        }

        [HttpPost("{id:guid}/images")]
        public async Task<IActionResult> AssignImage(Guid id, BookImageCreateDto dto, CancellationToken ct = default)
        {
            await _bookSvc.AssignImageAsync(id, dto, ct);
            return NoContent();
        }
    }
}
