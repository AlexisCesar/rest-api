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
            return await _context.ContratosCLT.Include(nameof(Contrato.Funcionario)).AsNoTracking().Where(x => x.Funcionario.Id == funcionarioId).ToListAsync();
        }

        public async Task<IEnumerable<ContratoPJ>> GetContratosPJAsync(Guid funcionarioId)
        {
            return await _context.ContratosPJ.Include(nameof(Contrato.Funcionario)).AsNoTracking().Where(x => x.Funcionario.Id == funcionarioId).ToListAsync();
        }
    }
}