using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookGallery.Application.DTOs;
using BookGallery.Application.DTOs.Author;
using BookGallery.Application.DTOs.BookImage;
using BookGallery.Domain.Entities;

namespace BookGallery.Application.Mappings
{
    public class MappingInitializer : Profile
    {
        public MappingInitializer()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, BookCreateDto>().ReverseMap();

            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Author, AuthorCreateDto>().ReverseMap();

            CreateMap<BookImage, BookImageDto>().ReverseMap();
            CreateMap<BookImage, BookImageCreateDto>().ReverseMap();
        }
    }

}