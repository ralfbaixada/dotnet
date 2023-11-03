using Application.ViewModels;
using AutoMapper;
using Bases.Entities;

namespace MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOperatorViewModel, OperatorViewModel>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<Operator, GetAllOperatorsResponse>();

            CreateMap<CreateOperatorViewModel, Operator>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<string, LoginOperatorResponse>()
                .ForMember(dest => dest.AcessToken, opt => opt.MapFrom(src => src));

            CreateMap<string, RenewTokenResponse>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src));

            //CreateMap<CreateRolesRequest, Role>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName));
        }
    }
}
