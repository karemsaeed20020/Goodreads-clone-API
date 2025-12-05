using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Reviews
{
    public class ReviewsMappingProfile : Profile
    {
        public ReviewsMappingProfile()
        {
            CreateMap<BookReview, BookReviewDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
        }
    }
}
