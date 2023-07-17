using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Authors;
using BookStoreApp.API.Models.Books;

namespace BookStoreApp.API.Configurations
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorsCreateDto,Author>().ReverseMap();

            CreateMap<AuthorUpdateDto,Author>().ReverseMap();

            CreateMap<AuthorReadonlyDto,Author>().ReverseMap();

            CreateMap<BooksCreateData,Book>().ReverseMap();

            CreateMap<BookUpdateDTO,Book>().ReverseMap();

            CreateMap<Book,BookReadOnlyDto>()
                .ForMember(q=>q.AuthorName,d=> d.MapFrom(map => $"{map.Author.FirstName}{map.Author.LastName}"))
                .ReverseMap();
            CreateMap<Book,BookDetailDto>()
                   .ForMember(q =>q.AuthorName,d=>d.MapFrom(map => $"{map.Author.FirstName}{map.Author.LastName}"))
                   .ReverseMap();







        }
    }
}
