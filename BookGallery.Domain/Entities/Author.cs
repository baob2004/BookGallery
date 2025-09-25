using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookGallery.Domain.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public DateOnly? Birthday { get; set; }
        public ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
    }
}