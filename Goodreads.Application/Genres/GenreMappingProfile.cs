using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Genres
{
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            CreateMap<Genre, GenreDto>()
                .ForMember(dest => dest.BookCount, opt => opt.MapFrom(src => src.BookGenres.Count));
        }
    }
}
