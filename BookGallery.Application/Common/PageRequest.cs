using System.ComponentModel;

namespace BookGallery.Application.Common
{
    public class PageRequest
    {
        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;
        [DefaultValue(20)]
        public int PageSize { get; set; } = 20;
        public string? SortBy { get; set; }
        public bool Desc { get; set; }
    }
}