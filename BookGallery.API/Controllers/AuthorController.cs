using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookGallery.Application.DTOs.Author;
using BookGallery.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookGallery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorSvc;
        public AuthorController(IAuthorService authSvc)
        {
            _authorSvc = authSvc;
        }

        [HttpGet]
        public async Task<ActionResult<AuthorDto>> GetAll(CancellationToken ct = default)
        {
            var res = await _authorSvc.GetAllAsync(ct);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetById(Guid id, CancellationToken ct = default)
        {
            var res = await _authorSvc.GetByIdAsync(id, ct);
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Create(AuthorCreateDto dto, CancellationToken ct = default)
        {
            var res = await _authorSvc.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, AuthorUpdateDto dto, CancellationToken ct = default)
        {
            await _authorSvc.UpdateAsync(id, dto, ct);
            return NoContent();
        }
    }
}