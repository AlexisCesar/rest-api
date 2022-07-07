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
        }
    }
}
