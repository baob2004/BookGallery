using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookGallery.Domain.Entities;

namespace BookGallery.Application.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {

    }
}