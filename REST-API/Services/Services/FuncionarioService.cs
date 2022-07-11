using AutoMapper;
using Entidades.Models;
using Restful_API.Data;
using Restful_API.Services.Interfaces;
using Restful_API.DTOs;

namespace Restful_API.Services.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IMapper _mapper;
        public FuncionarioService(IFuncionarioRepository funcionarioRepository, IMapper mapper)
        {
            _funcionarioRepository = funcionarioRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _funcionarioRepository.DeleteFuncionario(id);
            await _funcionarioRepository.SaveAsync();
        }

        public async Task<List<FuncionarioDTO>> GetAllAsync()
        {
            var funcionarios = await _funcionarioRepository.GetFuncionariosAsync();

            var funcionarioDTOs = _mapper.Map<List<FuncionarioDTO>>(funcionarios);

            return funcionarioDTOs;
        }

        public async Task<FuncionarioDTO> GetByIdAsync(Guid id)
        {
            var funcionario = await _funcionarioRepository.GetFuncionarioByIdAsync(id);

            var funcionarioDTO = _mapper.Map<FuncionarioDTO>(funcionario);

            return funcionarioDTO;
        }

        public async Task<FuncionarioDTO> InsertAsync(CreateFuncionarioRequest funcionario)
        {
            var funcionarioToInsert = new Funcionario(funcionario.Nome, funcionario.Email);

            await _funcionarioRepository.InsertFuncionarioAsync(funcionarioToInsert);
            await _funcionarioRepository.SaveAsync();

            return _mapper.Map<FuncionarioDTO>(funcionarioToInsert);
        }

        public async Task<FuncionarioDTO> UpdateAsync(UpdateFuncionarioRequest funcionario, Guid id)
        {
            var funcionarioToUpdate = await _funcionarioRepository.GetFuncionarioByIdAsync(id);

            funcionarioToUpdate.SetNome(funcionario.Nome);
            funcionarioToUpdate.SetEmail(funcionario.Email);

            _funcionarioRepository.UpdateFuncionario(funcionarioToUpdate);
            await _funcionarioRepository.SaveAsync();

            return _mapper.Map<FuncionarioDTO>(funcionarioToUpdate);
        }
    }
}
