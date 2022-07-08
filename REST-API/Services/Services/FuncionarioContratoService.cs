using AutoMapper;
using Restful_API.Data;
using Restful_API.DTOs;
using Restful_API.Services.Interfaces;

namespace Restful_API.Services.Services
{
    public class FuncionarioContratoService : IFuncionarioContratoService
    {
        private readonly IFuncionarioContratoRepository _repository;
        private readonly IMapper _mapper;
        public FuncionarioContratoService(
            IFuncionarioContratoRepository repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ContratoCLTDTO>> GetContratosCLTAsync(Guid funcionarioId)
        {
            var contratos = await _repository.GetContratosCLTAsync(funcionarioId);
            var contratosDTOs = _mapper.Map<IEnumerable<ContratoCLTDTO>>(contratos);
            return contratosDTOs;
        }

        public async Task<IEnumerable<ContratoPJDTO>> GetContratosPJAsync(Guid funcionarioId)
        {
            var contratos = await _repository.GetContratosPJAsync(funcionarioId);
            var contratosDTOs = _mapper.Map<IEnumerable<ContratoPJDTO>>(contratos);
            return contratosDTOs;
        }
    }
}