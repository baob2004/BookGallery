using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookGallery.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public DateOnly? ReleaseDate { get; set; }
        public ICollection<BookImage> BookImages { get; set; } = new List<BookImage>();
        public ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}