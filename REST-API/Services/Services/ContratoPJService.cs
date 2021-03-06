using AutoMapper;
using Entidades.Models;
using Restful_API.Data;
using Restful_API.DTOs;
using Restful_API.Services.Interfaces;

namespace Restful_API.Services.Services
{
    public class ContratoPJService : IContratoPJService
    {
        private readonly IContratoPJRepository _contratoRepository;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IMapper _mapper;
        public ContratoPJService(
            IContratoPJRepository contratoRepository,
            IFuncionarioService funcionarioService,
            IMapper mapper
            )
        {
            _contratoRepository = contratoRepository;
            _funcionarioService = funcionarioService;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _contratoRepository.DeleteContrato(id);
        }

        public async Task<List<ContratoPJDTO>> GetAllAsync()
        {
            var contratos = await _contratoRepository.GetContratosAsync();

            var contratosDTOs = _mapper.Map<List<ContratoPJDTO>>(contratos);

            return contratosDTOs;
        }

        public async Task<ContratoPJDTO> GetByIdAsync(Guid id)
        {
            var contrato = await _contratoRepository.GetContratoByIdAsync(id);

            var contratoDTO = _mapper.Map<ContratoPJDTO>(contrato);

            return contratoDTO;
        }

        public async Task<ContratoPJDTO?> InsertAsync(CreateContratoPJRequest contrato)
        {
            if (!await VerificarSeFuncionarioExiste(contrato.FuncionarioId)) return null;

            var contratoToInsert = new ContratoPJ(
                DateTime.UtcNow,
                null,
                contrato.SalarioBruto,
                contrato.Cargo ?? "-",
                contrato.FuncionarioId
                );

            await _contratoRepository.InsertContratoAsync(contratoToInsert);

            return _mapper.Map<ContratoPJDTO>(contratoToInsert);
        }

        private async Task<bool> VerificarSeFuncionarioExiste(Guid funcionarioId)
        {
            var funcionario = await _funcionarioService.GetByIdAsync(funcionarioId);

            return funcionario == null ? false : true;
        }

        public async Task<ContratoPJDTO> UpdateAsync(UpdateContratoPJRequest contrato, Guid id)
        {
            var contratoToUpdate = _mapper.Map<ContratoPJ>(contrato);

            var contratoInDatabase = await _contratoRepository.GetContratoByIdAsync(id);

            contratoInDatabase.SetCargo(contratoToUpdate.Cargo ?? "-");
            contratoInDatabase.SetSalarioBruto(contratoToUpdate.SalarioBruto);
            contratoInDatabase.SetTermino(contratoToUpdate.Termino);

            await _contratoRepository.UpdateContrato(contratoInDatabase);

            return _mapper.Map<ContratoPJDTO>(contratoInDatabase);
        }
    }
}