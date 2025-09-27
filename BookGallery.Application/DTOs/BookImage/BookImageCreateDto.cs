using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookGallery.Application.DTOs.BookImage
{
    public class BookImageCreateDto
    {
        public string Url { get; set; } = default!;
        public string? Caption { get; set; }
        public bool IsCover { get; set; }
    }
}