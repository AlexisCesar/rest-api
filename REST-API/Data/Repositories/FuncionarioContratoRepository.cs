using Entidades.Models;
using Microsoft.EntityFrameworkCore;

namespace Restful_API.Data
{
    public interface IFuncionarioContratoRepository
    {
        Task<IEnumerable<ContratoCLT>> GetContratosCLTAsync(Guid funcionarioId);
        Task<IEnumerable<ContratoPJ>> GetContratosPJAsync(Guid funcionarioId);
    }
    public class FuncionarioContratoRepository : IFuncionarioContratoRepository
    {
        private AppDbContext _context;
        public FuncionarioContratoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ContratoCLT>> GetContratosCLTAsync(Guid funcionarioId)
        {
            return await _context.ContratosCLT.AsNoTracking().Where(x => x.FuncionarioId == funcionarioId).ToListAsync();
        }

        public async Task<IEnumerable<ContratoPJ>> GetContratosPJAsync(Guid funcionarioId)
        {
            return await _context.ContratosPJ.AsNoTracking().Where(x => x.FuncionarioId == funcionarioId).ToListAsync();
        }
    }
}