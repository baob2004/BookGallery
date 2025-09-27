using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookGallery.Application.DTOs.Author
{
    public class AuthorCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public DateOnly? Birthday { get; set; }
    }
}