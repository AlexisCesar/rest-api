using Entidades.Models;
using Microsoft.EntityFrameworkCore;

namespace Restful_API.Data
{
    public interface IContratoPJRepository
    {
        Task<IEnumerable<ContratoPJ>> GetContratosAsync();
        Task<ContratoPJ> GetContratoByIdAsync(Guid contratoId);
        Task InsertContratoAsync(ContratoPJ contrato);
        Task DeleteContrato(Guid contratoId);
        void UpdateContrato(ContratoPJ contrato);
        Task SaveAsync();
    }

    public class ContratoPJRepository : IContratoPJRepository, IDisposable
    {
        private AppDbContext _context;
        public ContratoPJRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteContrato(Guid contratoId)
        {
            ContratoPJ contrato = await GetContratoByIdAsync(contratoId);
            _context.ContratosPJ.Remove(contrato);
        }

        public async Task<ContratoPJ> GetContratoByIdAsync(Guid contratoId)
        {
            return await _context.ContratosPJ.Include(nameof(Contrato.Funcionario)).AsNoTracking().FirstOrDefaultAsync(x => x.Id == contratoId);
        }

        public async Task<IEnumerable<ContratoPJ>> GetContratosAsync()
        {
            return await _context.ContratosPJ.Include(nameof(Contrato.Funcionario)).AsNoTracking().ToListAsync();
        }

        public async Task InsertContratoAsync(ContratoPJ contrato)
        {
            _context.Attach(contrato.Funcionario);
            await _context.ContratosPJ.AddAsync(contrato);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateContrato(ContratoPJ contrato)
        {
            _context.Entry(contrato).State = EntityState.Modified;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}