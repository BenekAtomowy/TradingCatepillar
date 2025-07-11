using AutoMapper;
using System.Linq.Expressions;
using TradingCatepillar.Integration.GoogleGemini.Models;
using TradingCatepillar.Persistence.Models;

namespace TradingCatepillar.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InstrumentRecommendation, Recommendation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreateTime, opt => opt.Ignore())
                .ForMember(dest => dest.RecommendationType, opt => opt.MapFrom(src => Enum.Parse<RecommendationType>(src.Recommendation!, true)));
        }
    }
}
