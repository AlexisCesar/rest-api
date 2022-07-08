using Restful_API.DTOs;

namespace Restful_API.Services.Interfaces
{
    public interface IFuncionarioContratoService
    {
        Task<IEnumerable<ContratoCLTDTO>> GetContratosCLTAsync(Guid funcionarioId);
        Task<IEnumerable<ContratoPJDTO>> GetContratosPJAsync(Guid funcionarioId);
    }
}