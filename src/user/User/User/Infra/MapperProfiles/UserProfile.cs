using AutoMapper;
using User.Api.Dto.Request;
using User.Api.Dto.Response;
using User.Application.Commands.CreateAnUser;
using User.Application.Commands.GetAnUser;

namespace User.Api.Infra.MapperProfiles
{
    /// <summary>
    /// Perfil de mapeamento de usuario
    /// </summary>
    public class UserProfile : Profile
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public UserProfile()
        {
            CreateMap<CreateAnUserRequest, CreateAnUserCommand>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<CreateAnUserCommandResponse, UserResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<GetAnUserCommandResponse, UserResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
