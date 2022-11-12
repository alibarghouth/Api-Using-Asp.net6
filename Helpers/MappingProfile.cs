using Add_Database_Model.Dtos;
using AutoMapper;

namespace Add_Database_Model.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MoviesDetailsDto>();
            CreateMap<MoviesDto, Movie>()
                .ForMember(src => src.Poster, option => option.Ignore());
        }
    }
}
