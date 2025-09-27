using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookGallery.Application.DTOs
{
    public class BookCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public DateOnly? ReleaseDate { get; set; }
    }
}