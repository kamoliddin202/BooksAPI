using AutoMapper;
using DataAccessLayer.Entities;
using DTOs.BookDtos;
using DTOs.CategoryDtos;

namespace DTOs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(c => c)));

            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<Book, BookDto>();
            CreateMap<AddBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();

        }
    }
}
