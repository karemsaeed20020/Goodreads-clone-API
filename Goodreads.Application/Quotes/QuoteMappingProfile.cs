using Goodreads.Application.Quotes.Commands.CreateQuote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Quotes
{
    public class QuoteMappingProfile : Profile
    {
        public QuoteMappingProfile()
        {
            CreateMap<CreateQuoteCommand, Quote>();
            CreateMap<Quote, QuoteDto>()
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.LikesCount));
        }
    }
}
