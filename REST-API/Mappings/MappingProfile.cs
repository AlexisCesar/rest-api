using AutoMapper;
using Entidades.Models;
using Restful_API.DTOs;

namespace Restful_API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Funcionario, FuncionarioDTO>().ReverseMap();
            CreateMap<CreateFuncionarioRequest, Funcionario>().ReverseMap();
            CreateMap<UpdateFuncionarioRequest, Funcionario>().ReverseMap();

            CreateMap<ContratoCLT, ContratoCLTDTO>()
            .ForMember(dest => dest.FuncionarioId, map =>
                    map.MapFrom(src => src.Funcionario.Id))
            .ReverseMap();

            CreateMap<CreateContratoCLTRequest, ContratoCLT>().ReverseMap();
            CreateMap<UpdateContratoCLTRequest, ContratoCLT>().ReverseMap();
            CreateMap<ContratoCLTDTO, UpdateContratoCLTRequest>().ReverseMap();

            CreateMap<ContratoPJ, ContratoPJDTO>()
            .ForMember(dest => dest.FuncionarioId, map =>
                    map.MapFrom(src => src.Funcionario.Id))
            .ReverseMap();

            CreateMap<CreateContratoPJRequest, ContratoPJ>().ReverseMap();
            CreateMap<UpdateContratoPJRequest, ContratoPJ>().ReverseMap();
            CreateMap<ContratoPJDTO, UpdateContratoPJRequest>().ReverseMap();
        }
    }
}
