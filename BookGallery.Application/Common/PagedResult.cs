using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookGallery.Application.Common
{
    public class PagedResult<T> where T : class
    {
        public List<T> Items { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}