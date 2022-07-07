using Restful_API.DTOs;

namespace Restful_API.Services.Interfaces
{
    public interface IFuncionarioService
    {
        Task<List<FuncionarioDTO>> GetAllAsync();
        Task<FuncionarioDTO> GetByIdAsync(Guid id);
        Task<FuncionarioDTO> InsertAsync(CreateFuncionarioRequest funcionario);
        Task DeleteAsync(Guid id);
        Task<FuncionarioDTO> UpdateAsync(UpdateFuncionarioRequest funcionario, Guid id);
    }
}
