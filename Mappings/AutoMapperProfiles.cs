using AutoMapper;
using BookManagementApi.Models.Domain;
using BookManagementApi.Models.DTO;

namespace BookManagementApi.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Book, BookDto>()
               .ForMember(dest => dest.PopularityScore, opt => opt.MapFrom(src =>
                   (src.ViewsCount * 0.5) + ((DateTime.Now.Year - src.PublicationYear) * 2)));
            CreateMap<Book, AddBookRequestDto>()
               .ReverseMap();
            CreateMap<Book,  UpdateBookRequestDto>()
               .ReverseMap();
        }
    }
    
}
