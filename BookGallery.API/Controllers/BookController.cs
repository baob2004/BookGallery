using System.Runtime.CompilerServices;
using AutoMapper;
using BookGallery.Application.Common;
using BookGallery.Application.DTOs;
using BookGallery.Application.Interfaces;
using BookGallery.Domain.Entities;
using BookGallery.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAll([FromQuery] PageRequest request)
        {
            try
            {
                var res = await _bookSvc.GetPagedAsync(request);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var res = await _bookSvc.GetByIdAsync(id);
                return res == null ? NotFound() : Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var res = await _bookSvc.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] BookUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var res = await _bookSvc.UpdateAsync(id, dto);
                if (!res) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
