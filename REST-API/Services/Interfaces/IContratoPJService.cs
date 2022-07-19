using Restful_API.DTOs;

namespace Restful_API.Services.Interfaces
{
    public interface IContratoPJService
    {
        Task<List<ContratoPJDTO>> GetAllAsync();
        Task<ContratoPJDTO> GetByIdAsync(Guid id);
        Task<ContratoPJDTO?> InsertAsync(CreateContratoPJRequest contrato);
        Task DeleteAsync(Guid id);
        Task<ContratoPJDTO> UpdateAsync(UpdateContratoPJRequest contrato, Guid id);
    }
}
