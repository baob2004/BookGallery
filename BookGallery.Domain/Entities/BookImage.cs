using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookGallery.Domain.Entities
{
    public class BookImage
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }
        public Book? Book { get; set; }
        public string Url { get; set; } = default!;
        public string? Caption { get; set; }
        public bool IsCover { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}