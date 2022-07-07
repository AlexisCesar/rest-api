using Restful_API.DTOs;

namespace Restful_API.Services.Interfaces
{
    public interface IContratoCLTService
    {
        Task<List<ContratoCLTDTO>> GetAllAsync();
        Task<ContratoCLTDTO> GetByIdAsync(Guid id);
        Task<ContratoCLTDTO> InsertAsync(CreateContratoCLTRequest contrato);
        Task DeleteAsync(Guid id);
        Task<ContratoCLTDTO> UpdateAsync(UpdateContratoCLTRequest contrato, Guid id);
    }
}
