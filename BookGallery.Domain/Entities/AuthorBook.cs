using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookGallery.Domain.Entities
{
    public class AuthorBook
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }
        public Book? Book { get; set; }
        public Author? Author { get; set; }
    }
}