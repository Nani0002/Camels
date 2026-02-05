using AutoMapper;
using Camels.DataAccess.Models;
using Camels.Shared.Models;

namespace Camels.WebAPI.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Camel, CamelResponseDto>();
            CreateMap<CamelRequestDto, Camel>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore()); ;
        }
    }
}
