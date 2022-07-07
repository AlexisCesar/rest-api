using AutoMapper;
using Entidades.Models;
using Restful_API.Data;
using Restful_API.DTOs;
using Restful_API.Services.Interfaces;

namespace Restful_API.Services.Services
{
    public class ContratoCLTService : IContratoCLTService
    {
        private readonly IContratoCLTRepository _contratoRepository;
        private readonly IMapper _mapper;
        public ContratoCLTService(IContratoCLTRepository contratoRepository, IMapper mapper)
        {
            _contratoRepository = contratoRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _contratoRepository.DeleteContrato(id);
            await _contratoRepository.SaveAsync();
        }

        public async Task<List<ContratoCLTDTO>> GetAllAsync()
        {
            var contratos = await _contratoRepository.GetContratosAsync();

            var contratosDTOs = _mapper.Map<List<ContratoCLTDTO>>(contratos);

            return contratosDTOs;
        }

        public async Task<ContratoCLTDTO> GetByIdAsync(Guid id)
        {
            var contrato = await _contratoRepository.GetContratoByIdAsync(id);

            var contratoDTO = _mapper.Map<ContratoCLTDTO>(contrato);

            return contratoDTO;
        }

        public async Task<ContratoCLTDTO> InsertAsync(CreateContratoCLTRequest contrato)
        {
            var contratoToInsert = _mapper.Map<ContratoCLT>(contrato);

            contratoToInsert.Id = Guid.NewGuid();
            contratoToInsert.Inicio = DateTime.UtcNow;

            contratoToInsert.Funcionario = new Funcionario() { Id = contrato.FuncionarioId };

            await _contratoRepository.InsertContratoAsync(contratoToInsert);
            await _contratoRepository.SaveAsync();

            return _mapper.Map<ContratoCLTDTO>(contratoToInsert);
        }

        public async Task<ContratoCLTDTO> UpdateAsync(UpdateContratoCLTRequest contrato, Guid id)
        {
            var contratoToUpdate = _mapper.Map<ContratoCLT>(contrato);

            var contratoInDatabase = await _contratoRepository.GetContratoByIdAsync(id);

            contratoInDatabase.Cargo = contratoToUpdate.Cargo;
            contratoInDatabase.SalarioBruto = contratoToUpdate.SalarioBruto;
            contratoInDatabase.Termino = contratoToUpdate.Termino;

            _contratoRepository.UpdateContrato(contratoInDatabase);
            await _contratoRepository.SaveAsync();

            return _mapper.Map<ContratoCLTDTO>(contratoInDatabase);
        }
    }
}