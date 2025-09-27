using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookGallery.Application.DTOs.BookImage
{
    public class BookImageDto : BookImageCreateDto
    {
        public Guid Id { get; set; }
    }
}