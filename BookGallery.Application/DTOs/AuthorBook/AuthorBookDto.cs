using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookGallery.Application.DTOs.AuthorBook
{
    public class AuthorBookDto
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }
    }
}